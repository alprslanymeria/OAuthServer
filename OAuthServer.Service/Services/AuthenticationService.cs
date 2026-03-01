using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OAuthServer.Core.Configuration;
using OAuthServer.Core.DTOs.Client;
using OAuthServer.Core.DTOs.RefreshToken;
using OAuthServer.Core.DTOs.User;
using OAuthServer.Core.Exceptions;
using OAuthServer.Core.Helper;
using OAuthServer.Core.Models;
using OAuthServer.Core.Repositories;
using OAuthServer.Core.Services;
using OAuthServer.Core.UnitOfWork;

namespace OAuthServer.Service.Services;

public class AuthenticationService(

    UserManager<User> userManager,
    ITokenService tokenService,
    IUnitOfWork unitOfWork,
    IOptions<List<Client>> optionsClient,
    IGenericRepository<UserRefreshToken> userRefreshTokenRepository
    
    ) : IAuthenticationService
{
    private readonly List<Client> _clients = optionsClient.Value;
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository = userRefreshTokenRepository;

    public async Task<ServiceResult<TokenResponse>> CreateTokenAsync(SignInRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        // FIND USER BY EMAIL
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null) throw new UnauthorizedException("Invalid credentials.");

        // VALIDATE PASSWORD
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!passwordValid) throw new UnauthorizedException("Invalid credentials.");

        // CREATE TOKEN AND SAVE REFRESH TOKEN
        var token = _tokenService.CreateToken(user);
        await SaveOrUpdateRefreshTokenAsync(user.Id, token);

        return ServiceResult<TokenResponse>.Success(token);

    }

    public async Task<ServiceResult<TokenResponse>> CreateTokenByRefreshToken(string refreshToken)
    {

        // CHECK REFRESH TOKEN & USER
        var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync()
            ?? throw new NotFoundException("Refresh token not found.");

        var user = await _userManager.FindByIdAsync(existRefreshToken.UserId)
            ?? throw new NotFoundException("User not found.");

        // CREATE TOKEN
        var token = _tokenService.CreateToken(user);

        // UPDATE REFRESH TOKEN
        existRefreshToken.Code = token.RefreshToken;
        existRefreshToken.Expiration = token.RefreshTokenExpiration;

        // SINCE THE DATA RETURNED WITH THE WHERE CONDITION WAS MARKED AS "NO TRACKING" I CALLED IT USING THE UPDATE METHOD TO ENABLE TRACKING.
        // OTHERWISE, THE CHANGES WON'T BE REFLECTED IN THE DATABASE WHEN CALLING COMMIT ASYNC.
        _userRefreshTokenRepository.Update(existRefreshToken);
        await _unitOfWork.CommitAsync();

        return ServiceResult<TokenResponse>.Success(token);
    }

    public async Task<ServiceResult> RevokeRefreshToken(string refreshToken)
    {
        var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync()
            ?? throw new NotFoundException("Refresh token not found.");

        _userRefreshTokenRepository.Delete(existRefreshToken);
        await _unitOfWork.CommitAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<ClientTokenResponse>> CreateTokenByClient(ClientSignInRequest request)
    {
        var client = _clients.SingleOrDefault(x => x.Id == request.ClientId && x.Secret == request.ClientSecret)
            ?? throw new NotFoundException("Client not found.");

        var token = _tokenService.CreateTokenByClient(client);

        return ServiceResult<ClientTokenResponse>.Success(token);
    }

    public async Task<ServiceResult<TokenResponse>> CreateTokenByExternalLogin(string email, string? name, string googleSubjectId, string? picture)
    {
        // FIND USER BY EMAIL
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            // CREATE USER IF NOT FOUND
            user = new User
            {
                UserName = email.Split('@')[0],
                Email = email,
                EmailConfirmed = true,
                Image = picture
            };

            // CREATE RANDOM PASSWORD FOR EXTERNAL LOGIN USER
            var password = Guid.NewGuid().ToString("N") + "Ax1!";
            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                throw new BusinessException(createResult.Errors.Select(e => e.Description).ToList());
            }

            // ADD GOOGLE LOGIN PROVIDER
            var loginInfo = new UserLoginInfo("Google", googleSubjectId, "Google");
            await _userManager.AddLoginAsync(user, loginInfo);
        }

        // CREATE TOKEN AND SAVE REFRESH TOKEN
        var token = _tokenService.CreateToken(user);
        await SaveOrUpdateRefreshTokenAsync(user.Id, token);

        return ServiceResult<TokenResponse>.Success(token);
    }


    #region HELPERS

    private async Task SaveOrUpdateRefreshTokenAsync(string userId, TokenResponse token)
    {
        var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.UserId == userId).SingleOrDefaultAsync();

        if (userRefreshToken is null)
        {
            await _userRefreshTokenRepository.AddAsync(new UserRefreshToken
            {
                UserId = userId,
                Code = token.RefreshToken,
                Expiration = token.RefreshTokenExpiration
            });
        }
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
            _userRefreshTokenRepository.Update(userRefreshToken);
        }

        await _unitOfWork.CommitAsync();
    }

    #endregion
}