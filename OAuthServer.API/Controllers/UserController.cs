using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Services;

namespace OAuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(

        IUserService userService) : BaseController
    {
        private readonly IUserService _userService = userService;

        // TESTED WITH POSTMAN ✅
        [HttpPost]
        public async Task<IActionResult> CreateUser(SignUpDto signUpDto)
        {
            var result = await _userService.CreateUserAsync(signUpDto);

            return ActionResultInstance(result);
        }
    }
}
