using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OAuthServer.Core.DTOs;
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
    IGenericRepository<UserRefreshToken> userRefreshTokenRepository) : IAuthenticationService
{
    //private readonly List<Client> _clients = optionsClient.Value;
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

        // WHERE KOŞULU İLE GELEN DATA NO TRACKING OLARAK GELDİĞİ İÇİN TRACK EDİLMESİ İÇİN UPDATE METODU İLE ÇAĞIRDIM.
        // YOKSA COMMIT ASYNC ÇAĞRISINDA DEĞİŞİKLİKLER VERİTABANINA YANSIMAZ.
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

    //public Response<ClientTokenDto> CreateTokenByClient(ClientSignInDto clientSignInDto)
    //{
    //    var client = _clients.SingleOrDefault(X => x.Id == clientSignInDto.ClientId && x.Secret == clientSignInDto.ClientSecret);

    //    if(client is null)
    //    {
    //        return Response<ClientTokenDto>.Fail("ClientId or ClientSecret not found", true, 404);
    //    }

    //    var token = _tokenService.CreateTokenByClient(client);

    //    return Response<ClientTokenDto>.Success(token, 200);
    //}
}