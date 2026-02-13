using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Core.DTOs.Extra;
using OAuthServer.Core.DTOs.User;
using OAuthServer.Core.Services;
using System.Security.Claims;

namespace OAuthServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(

    IUserService userService) : BaseController
{
    private readonly IUserService _userService = userService;
    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

    [HttpPost]
    public async Task<IActionResult> CreateUser(SignUpRequest request) 
        => ActionResultInstance(await _userService.CreateUserAsync(request));

    [Authorize]
    [HttpGet("by-name/{username:minlength(3)}")]
    public async Task<IActionResult> GetUserByUserName(string username) 
        => ActionResultInstance(await _userService.GetUserByUserNameAsync(username));

    // EXTRA METHODS FOR MY NEXTJS PROJECT
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetProfileInfos() 
        => ActionResultInstance(await _userService.GetProfileInfos(UserId));

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateProfileInfos([FromForm]  UpdateProfileInfosRequest request) 
        => ActionResultInstance(await _userService.UpdateProfileInfos(request));

    [Authorize]
    [HttpPost("compare-language")]
    public async Task<IActionResult> CompareLanguageId(CompareLanguageIdRequest request) 
        => ActionResultInstance(await _userService.CompareLanguageId(request));
}