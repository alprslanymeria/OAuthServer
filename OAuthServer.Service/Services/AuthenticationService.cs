using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OAuthServer.Core.Configuration;
using OAuthServer.Core.DTOs.Client;
using OAuthServer.Core.DTOs.RefreshToken;
using OAuthServer.Core.DTOs.User;
using OAuthServer.Core.Helper;
using OAuthServer.Core.Models;
using OAuthServer.Core.Repositories;
using OAuthServer.Core.Services;
using OAuthServer.Core.UnitOfWork;
using System.Net;

namespace OAuthServer.Service.Services;

public class AuthenticationService(

    //IOptions<List<Client>> optionsClient,
    UserManager<User> userManager,
    ITokenService tokenService,
    IUnitOfWork unitOfWork,
    IOptions<List<Client>> optionsClient,
    IGenericRepository<UserRefreshToken> userRefreshTokenRepository) : IAuthenticationService
{
    private readonly List<Client> _clients = optionsClient.Value;
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository = userRefreshTokenRepository;

    public async Task<Response<TokenResponse>> CreateTokenAsync(SignInRequest request)
    {
        // CHECK SIGNIN DTO
        ArgumentNullException.ThrowIfNull(request);

        // GET USER BY EMAIL
        var user = await _userManager.FindByEmailAsync(request.Email);

        // CHEK USER
        if (user is null)
        {
            return Response<TokenResponse>.Fail("Invalid email or password.");
        }

        // GET PASSWORD
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
        {
            return Response<TokenResponse>.Fail("Invalid email or password.");
        }

        // CREATE TOKEN
        var token = _tokenService.CreateToken(user);

        // CHECK REFRESH TOKEN
        var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

        if (userRefreshToken is null)
        {
            await _userRefreshTokenRepository.AddAsync(new UserRefreshToken
            {
                UserId = user.Id,
                Code = token.RefreshToken,
                Expiration = token.RefreshTokenExpiration
            });
        }
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        }

        await _unitOfWork.CommitAsync();

        return Response<TokenResponse>.Success(token);

    }

    public async Task<Response<TokenResponse>> CreateTokenByRefreshToken(string refreshToken)
    {
        // CHECK REFRESH TOKEN
        var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

        if (existRefreshToken is null)
        {
            return Response<TokenResponse>.Fail("Refresh token not found", HttpStatusCode.NotFound);
        }

        // CHECK USER
        var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

        if (user is null)
        {
            return Response<TokenResponse>.Fail("User Id not found", HttpStatusCode.NotFound);
        }

        // CREATE TOKEN
        var token = _tokenService.CreateToken(user);

        // UPDATE REFRESH TOKEN
        existRefreshToken.Code = token.RefreshToken;
        existRefreshToken.Expiration = token.RefreshTokenExpiration;

        // SINCE THE DATA RETURNED WITH THE WHERE CONDITION WAS MARKED AS "NO TRACKING" I CALLED IT USING THE UPDATE METHOD TO ENABLE TRACKING.
        // OTHERWISE, THE CHANGES WON'T BE REFLECTED IN THE DATABASE WHEN CALLING COMMIT ASYNC.
        _userRefreshTokenRepository.Update(existRefreshToken);

        // UPDATE DATABASE
        await _unitOfWork.CommitAsync();

        return Response<TokenResponse>.Success(token);
    }

    public async Task<Response> RevokeRefreshToken(string refreshToken)
    {
        var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

        if (existRefreshToken is null)
        {
            return Response.Fail("Refresh token not found", HttpStatusCode.NotFound);
        }

        _userRefreshTokenRepository.Delete(existRefreshToken);

        await _unitOfWork.CommitAsync();

        return Response.Success();
    }

    public async Task<Response<ClientTokenResponse>> CreateTokenByClient(ClientSignInRequest request)
    {
        var client = _clients.SingleOrDefault(x => x.Id == request.ClientId && x.Secret == request.ClientSecret);

        if (client is null)
        {
            return Response<ClientTokenResponse>.Fail("ClientId or ClientSecret not found", HttpStatusCode.NotFound);
        }

        var token = _tokenService.CreateTokenByClient(client);

        return Response<ClientTokenResponse>.Success(token);
    }

    public async Task<Response<TokenResponse>> CreateTokenByExternalLogin(string email, string? name, string googleSubjectId, string? picture)
    {
        // FIND USER BY EMAIL
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            // CREATE USER IF NOT FOUND
            user = new User
            {
                UserName = name ?? email.Split('@')[0],
                Email = email,
                EmailConfirmed = true,
                Image = picture
            };

            // CREATE RANDOM PASSWORD FOR EXTERNAL LOGIN USER
            var password = Guid.NewGuid().ToString("N") + "Ax1!";
            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                var errors = createResult.Errors.Select(e => e.Description).ToList();
                return Response<TokenResponse>.Fail(errors, HttpStatusCode.BadRequest);
            }

            // ADD GOOGLE LOGIN PROVIDER
            var loginInfo = new UserLoginInfo("Google", googleSubjectId, "Google");
            await _userManager.AddLoginAsync(user, loginInfo);
        }

        // CREATE TOKEN
        var token = _tokenService.CreateToken(user);

        //  SAVE REFRESH TOKEN
        var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

        if (userRefreshToken is null)
        {
            await _userRefreshTokenRepository.AddAsync(new UserRefreshToken
            {
                UserId = user.Id,
                Code = token.RefreshToken,
                Expiration = token.RefreshTokenExpiration
            });
        }
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        }

        await _unitOfWork.CommitAsync();

        return Response<TokenResponse>.Success(token);
    }
}