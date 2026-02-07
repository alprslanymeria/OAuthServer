using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Core.DTOs.Client;
using OAuthServer.Core.DTOs.RefreshToken;
using OAuthServer.Core.DTOs.User;
using System.Security.Claims;

namespace OAuthServer.API.Controllers;

// "api/[controller]"           => MATCHING BY HTTP METHOD TYPE        => USE IN SIMPLE STRUCTURES.
// "api/[controller]/[action]"  => ACTION METHOD NAME MATCHING         => USE IN COMPLEX STRUCTURES.


[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController(

    Core.Services.IAuthenticationService authenticationService) : BaseController
{
    private readonly Core.Services.IAuthenticationService _authenticationService = authenticationService;

    [HttpPost]
    public async Task<IActionResult> CreateToken(SignInRequest request) 
        => ActionResultInstance(await _authenticationService.CreateTokenAsync(request));

    [HttpPost]
    public async Task<IActionResult> RevokeRefreshToken(RefreshTokenRequest request) 
        => ActionResultInstance(await _authenticationService.RevokeRefreshToken(request.Token));

    [HttpPost]
    public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenRequest request) 
        => ActionResultInstance(await _authenticationService.CreateTokenByRefreshToken(request.Token));

    [HttpPost]
    public async Task<IActionResult> CreateTokenByClient(ClientSignInRequest request) 
        => ActionResultInstance(await _authenticationService.CreateTokenByClient(request));
    
    [HttpGet]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(nameof(GoogleCallback))
        };

        return Challenge(properties, "Google");
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallback()
    {
        // READ GOOGLE USER INFO FROM EXTERNAL COOKIE
        var result = await HttpContext.AuthenticateAsync("ExternalCookie");

        if (!result.Succeeded || result.Principal is null)
        {
            return Unauthorized();
        }

        // PULL USER INFO FROM CLAIMS
        var email = result.Principal.FindFirstValue(ClaimTypes.Email);
        var name = result.Principal.FindFirstValue(ClaimTypes.Name);
        var googleSubjectId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var picture = result.Principal.FindFirst("picture")?.Value;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(googleSubjectId))
        {
            return BadRequest("Google'dan email veya kullanıcı bilgisi alınamadı.");
        }

        // DELETE THE TEMPORARY EXTERNAL COOKIE
        await HttpContext.SignOutAsync("ExternalCookie");

        // CREATE TOKEN
        var tokenResponse = await _authenticationService.CreateTokenByExternalLogin(email, name, googleSubjectId, picture);

        return ActionResultInstance(tokenResponse);
    }

}