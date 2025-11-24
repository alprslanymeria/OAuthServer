using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using OAuthServer.Core.DTOs;
using OAuthServer.Core.Helper;
using OAuthServer.Core.Models;
using OAuthServer.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Service.Services
{
    public class UserService(

        UserManager<User> userManager,
        IMapper mapper) : IUserService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<UserDto>> CreateUserAsync(SignUpDto signUpDto)
        {
            var user = new User
            {
                UserName = signUpDto.Username,
                Email = signUpDto.Email,
                NativeLanguageId = signUpDto.NativeLanguageId,
            };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded)
            {
                // GET ERROR MESSAGES
                var errors = result.Errors.Select(e => e.Description).ToList();

                var errorDto = new ErrorDto(errors, true);

                return Response<UserDto>.Fail(errorDto, 400);
            }

            // USER MAP TO USERDTO
            var userDto = _mapper.Map<UserDto>(user);

            return Response<UserDto>.Success(201);
        }
    }
}
