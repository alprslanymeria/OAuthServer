using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Services;

namespace OAuthServer.API.Controllers
{
    // "api/[controller]"           => METOT TİPİNE GÖRE EŞLEŞME OLUR.        => YAPILABİLİYORSA BU YAPILSIN.
    // "api/[controller]/[action]"  => ACTION METOT İSMİNE GÖRE EŞLEŞME OLUR. => KARMAŞIK YAPILARDA KULLANILSIN.


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(

        IAuthenticationService authenticationService) : BaseController
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;


        // TESTED WITH POSTMAN ✅
        // CREATE TOKEN
        [HttpPost]
        public async Task<IActionResult> CreateToken(SignInDto signInDto)
        {
            var result = await _authenticationService.CreateTokenAsync(signInDto);

            return ActionResultInstance(result);
        }

        // TESTED WITH POSTMAN ✅
        // REVOKE REFRESH TOKEN
        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshToken(refreshTokenDto.Token);

            return ActionResultInstance(result);
        }

        // TESTED WITH POSTMAN ✅
        // CREATE TOKEN BY REFRESH TOKEN
        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.Token);

            return ActionResultInstance(result);
        }

        //[HttpPost]
        //public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        //{
        //    var result = await _authenticationService.CreateTokenForClient(clientLoginDto);

        //    return ActionResultInstance(result);
        //}
    }
}
