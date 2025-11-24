using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OAuthServer.Core.Configuration;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Models;
using OAuthServer.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OAuthServer.Service.Services
{
    // BU TOKEN SERVICE'I SADECE BU ASSEMBLY İÇERİSİNDE KULLANACAĞIMIZ İÇİN "INTERNAL" KALMASI GEREKİYOR.
    // FAKAT API KATMMANINDA DI REGISTER OLARAK KAYIT ETMEMİZ GEREKTİĞİ İÇİN BUNU ŞUANLIK "PUBLIC" YAPACAĞIZ.
    // BUNUN BEST PRACTICE OLANI BU ASSEMBLY'E DE DI CONTAINER FRAMEWORK EKLEYEREK BURADA INITİALİZE ETMEK.
    public class TokenService(

        UserManager<User> userManager,
        IOptions<TokenOption> options) : ITokenService
    {

        private readonly UserManager<User> _userManager = userManager;
        private readonly TokenOption _tokenOption = options.Value;

        // PRIVATE METHOD FOR CREATIN REFREH TOKEN
        private static string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }


        // PRIVATE METHOD FOR CREATE CLAIMS
        private static IEnumerable<Claim> GetClaims(User user, List<String> audiences)
        {
            var claimList = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claimList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return claimList;
        }



        public TokenDto CreateToken(User user)
        {
            // BİZİM BELİRLEDİĞİMİZ TOKEN OPTION DEĞERLERİNİ ALIYORUZ
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var issuer = _tokenOption.Issuer;


            // JWT BİZDEN "SigningCredentials" NESNESİ İSTER. BU NESNE İSE BİZDEN "SecurityKey" İSTER.
            var securityKey = SignService.GetSymetricKey(_tokenOption.SecurityKey);
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // GET CLAIMS
            var claims = GetClaims(user, _tokenOption.Audience);

            // CREATE JWT TOKEN
            JwtSecurityToken jwtSecurityToken = new(

                issuer: issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = CreateRefreshToken(),
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }


        //private IEnumerable<Claim> GetClaimsForClient(Client client)
        //{
        //    var claimList = new List<Claim>

        //    claimList.AddRange(client.audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
        //    new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());

        //    return claimList;
        //}


        //public ClientTokenDto  CreateTokenByClient(Client client)
        //{
        //    // BİZİM BELİRLEDİĞİMİZ TOKEN OPTION DEĞERLERİNİ ALIYORUZ
        //    var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
        //    var issuer = _tokenOption.Issuer;


        //    // JWT BİZDEN "SigningCredentials" NESNESİ İSTER. BU NESNE İSE BİZDEN "SecurityKey" İSTER.
        //    var securityKey = SignService.GetSymetricKey(_tokenOption.SecurityKey);
        //    SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

        //    // GET CLAIMS
        //    var claims = GetClaimsForClient(client);

        //    // CREATE JWT TOKEN
        //    JwtSecurityToken jwtSecurityToken = new(

        //        issuer: issuer,
        //        expires: accessTokenExpiration,
        //        notBefore: DateTime.Now,
        //        claims: claims,
        //        signingCredentials: signingCredentials);

        //    var handler = new JwtSecurityTokenHandler();

        //    var token = handler.WriteToken(jwtSecurityToken);

        //    var clientTokenDto = new ClientTokenDto
        //    {
        //        AccessToken = token,
        //        AccessTokenExpiration = accessTokenExpiration
        //    };

        //    return clientTokenDto;
        //}
    }
}
