using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Helper;
using OAuthServer.Core.Models;
using OAuthServer.Core.Services;
using System.Net;

namespace OAuthServer.Service.Services;

public class UserService(

    UserManager<User> userManager,
    IMapper mapper) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<UserDto>> CreateUserAsync(SignUpRequest request)
    {
        // BURADA MANUEL MAPPING YAPTIK ÇÜNKÜ REQUEST ÖNEMLİ BİLGİLER İÇERİYOR.
        var user = new User
        {
            UserName = request.Username,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            // GET ERROR MESSAGES
            var errors = result.Errors.Select(e => e.Description).ToList();

            return Response<UserDto>.Fail(errors, HttpStatusCode.BadRequest);
        }

        // USER MAP TO USERDTO
        var userDto = _mapper.Map<UserDto>(user);

        return Response<UserDto>.SuccessAsCreated(userDto, $"api/user/{userDto.UserName}");
    }

    public async Task<Response<UserDto>> GetUserByUserNameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
        {
            return Response<UserDto>.Fail($"Kullanıcı bulunamadı: {username}", HttpStatusCode.NotFound);
        }

        var userDto = _mapper.Map<UserDto>(user);

        return Response<UserDto>.Success(userDto, HttpStatusCode.OK);
    }
}