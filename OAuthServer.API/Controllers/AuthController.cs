using Microsoft.AspNetCore.Mvc;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Services;

namespace OAuthServer.API.Controllers;

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
    public async Task<IActionResult> CreateToken(SignInRequest request) => ActionResultInstance(await _authenticationService.CreateTokenAsync(request));

    // TESTED WITH POSTMAN ✅
    // REVOKE REFRESH TOKEN
    [HttpPost]
    public async Task<IActionResult> RevokeRefreshToken(RefreshTokenRequest request) => ActionResultInstance(await _authenticationService.RevokeRefreshToken(request.Token));

    // TESTED WITH POSTMAN ✅
    // CREATE TOKEN BY REFRESH TOKEN
    [HttpPost]
    public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenRequest request) => ActionResultInstance(await _authenticationService.CreateTokenByRefreshToken(request.Token));

    //[HttpPost]
    //public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
    //{
    //    var result = await _authenticationService.CreateTokenForClient(clientLoginDto);

    //    return ActionResultInstance(result);
    //}
}