using Microsoft.AspNetCore.Mvc;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Services;

namespace OAuthServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(

    IUserService userService) : BaseController
{
    private readonly IUserService _userService = userService;

    // TESTED WITH POSTMAN ✅
    [HttpPost]
    public async Task<IActionResult> CreateUser(SignUpRequest request) =>  ActionResultInstance(await _userService.CreateUserAsync(request));

    [HttpGet("{username:minlength(3)}")]
    public async Task<IActionResult> GetUserByUserName(string username) => ActionResultInstance(await _userService.GetUserByUserNameAsync(username));
}