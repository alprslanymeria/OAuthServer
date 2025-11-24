using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Helper;
using OAuthServer.Core.Models;
using OAuthServer.Core.Repositories;
using OAuthServer.Core.Services;
using OAuthServer.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Service.Services
{
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

        public async Task<Response<TokenDto>> CreateTokenAsync(SignInDto signInDto)
        {
            // CHECK SIGNIN DTO
            ArgumentNullException.ThrowIfNull(signInDto);

            // GET USER BY EMAIL
            var user = await _userManager.FindByEmailAsync(signInDto.Email);

            // CHEK USER
            if (user == null)
            {
                return Response<TokenDto>.Fail("Invalid email or password.", true, 400);
            }

            // GET PASSWORD
            var passwordValid = await _userManager.CheckPasswordAsync(user, signInDto.Password);

            if (!passwordValid)
            {
                return Response<TokenDto>.Fail("Invalid email or password.", true, 400);
            }

            // CREATE TOKEN
            var token = _tokenService.CreateToken(user);

            // CHECK REFRESH TOKEN
            var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
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

            return Response<TokenDto>.Success(token, 200);

        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            // CHECK REFRESH TOKEN
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<TokenDto>.Fail("Refresh token not found", true, 404);
            }

            // CHECK USER
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return Response<TokenDto>.Fail("User Id not found", true, 404);
            }

            // CREATE TOKEN
            var token = _tokenService.CreateToken(user);

            // UPDATE REFRESH TOKEN
            existRefreshToken.Code = token.RefreshToken;
            existRefreshToken.Expiration = token.RefreshTokenExpiration;

            // UPDATE DATABASE
            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public async Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<NoDataDto>.Fail("Refresh token not found", true, 404);
            }

            _userRefreshTokenRepository.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(200);
        }

        //public Response<ClientTokenDto> CreateTokenByClient(ClientSignInDto clientSignInDto)
        //{
        //    var client = _clients.SingleOrDefault(X => x.Id == clientSignInDto.ClientId && x.Secret == clientSignInDto.ClientSecret);

        //    if(client == null)
        //    {
        //        return Response<ClientTokenDto>.Fail("ClientId or ClientSecret not found", true, 404);
        //    }

        //    var token = _tokenService.CreateTokenByClient(client);

        //    return Response<ClientTokenDto>.Success(token, 200);
        //}
    }
}
