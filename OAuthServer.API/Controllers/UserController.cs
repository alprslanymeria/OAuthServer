using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Core.DTOs.Extra;
using OAuthServer.Core.DTOs.User;
using OAuthServer.Core.Services;

namespace OAuthServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(

    IUserService userService) : BaseController
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> CreateUser(SignUpRequest request) 
        => ActionResultInstance(await _userService.CreateUserAsync(request));

    [Authorize]
    [HttpGet("by-name/{username:minlength(3)}")]
    public async Task<IActionResult> GetUserByUserName(string username) 
        => ActionResultInstance(await _userService.GetUserByUserNameAsync(username));

    // EXTRA METHODS FOR MY NEXTJS PROJECT
    [Authorize]
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetProfileInfos(string userId) 
        => ActionResultInstance(await _userService.GetProfileInfos(userId));

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateProfileInfos([FromForm]  UpdateProfileInfosRequest request) 
        => ActionResultInstance(await _userService.UpdateProfileInfos(request));

    [Authorize]
    [HttpGet("compare-language")]
    public async Task<IActionResult> CompareLanguageId(CompareLanguageIdRequest request) 
        => ActionResultInstance(await _userService.CompareLanguageId(request));
}