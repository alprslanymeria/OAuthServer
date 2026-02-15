using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OAuthServer.Core.DTOs.Extra;
using OAuthServer.Core.DTOs.User;
using OAuthServer.Core.Enum;
using OAuthServer.Core.Helper;
using OAuthServer.Core.Models;
using OAuthServer.Core.Services;
using System.Net;

namespace OAuthServer.Service.Services;

public class UserService(

    UserManager<User> userManager,
    IFileStorageHelper fileStorageHelper,
    IMapper mapper,
    ILogger<UserService> logger

    ) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IMapper _mapper = mapper;
    private readonly IFileStorageHelper _fileStorageHelper = fileStorageHelper;
    private readonly ILogger<UserService> _logger = logger;

    public async Task<ServiceResult<UserDto>> CreateUserAsync(SignUpRequest request)
    {
        // IN HERE, WE DID MANUAL MAPPING BECAUSE THE REQUEST CONTAINS IMPORTANT INFORMATION.
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            NativeLanguageId = request.NativeLanguageId
        };

        // CREATE USER WITH USER MANAGER
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            // GET ERROR MESSAGES
            var errors = result.Errors.Select(e => e.Description).ToList();

            return ServiceResult<UserDto>.Fail(errors, HttpStatusCode.BadRequest);
        }

        // USER MAP TO USERDTO
        var userDto = _mapper.Map<UserDto>(user);

        return ServiceResult<UserDto>.SuccessAsCreated(userDto, $"api/user/{userDto.UserName}");
    }

    public async Task<ServiceResult<UserDto>> GetUserByUserNameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
        {
            return ServiceResult<UserDto>.Fail($"Kullanıcı bulunamadı: {username}", HttpStatusCode.NotFound);
        }

        var userDto = _mapper.Map<UserDto>(user);

        return ServiceResult<UserDto>.Success(userDto, HttpStatusCode.OK);
    }

    // // EXTRA METHODS FOR MY NEXTJS PROJECT
    public async Task<ServiceResult<UserDto>> GetProfileInfos(string userId)
    {

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
        {
            return ServiceResult<UserDto>.Fail($"Kullanıcı bulunamadı: {userId} {user}", HttpStatusCode.NotFound);
        }

        var userDto = _mapper.Map<UserDto>(user);

        return ServiceResult<UserDto>.Success(userDto, HttpStatusCode.OK);
    }

    public async Task<ServiceResult> UpdateProfileInfos(UpdateProfileInfosRequest request)
    {
        // EXTRACT REQUEST DATA
        var userId = request.UserId;
        var name = request.Name;
        var nativeLanguageId = request.NativeLanguageId;
        var image = request.Image as IFileUpload;

        // LOG MESSAGE
        _logger.LogInformation($"UpdateProfileInfosHandler: Received request to update profile infos for User ID {userId}");

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return ServiceResult.Fail($"Kullanıcı bulunamadı: {userId}", HttpStatusCode.NotFound);
        }

        // STORE OLD FILE URLS FOR DELETION
        var oldImageUrl = user.Image;
        var newImageUrl = "";

        // UPDATE IMAGE IF NEW FILE PROVIDED
        if (image != null)
        {
            _logger.LogInformation($"UpdateProfileInfosHandler: Processing new profile image for User ID {userId}");

            newImageUrl = await _fileStorageHelper.UploadFileToStorageAsync(
                image,
                userId,
                "profile"
            );
        }

        // UPDATE PROFILE INFOS
        user.UserName = name;
        user.NativeLanguageId = nativeLanguageId;
        user.Image = newImageUrl;

        // UPDATE USER WITH USER MANAGER
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            // GET ERROR MESSAGES
            var errors = result.Errors.Select(e => e.Description).ToList();

            return ServiceResult.Fail(errors, HttpStatusCode.BadRequest);
        }

        // DELETE OLD FILES FROM STORAGE IF URLS CHANGED
        if (image != null && oldImageUrl?.ToLower() != newImageUrl?.ToLower())
        {
            await _fileStorageHelper.DeleteFileFromStorageAsync(oldImageUrl!);
        }

        // LOG MESSAGE
        _logger.LogInformation($"UpdateProfileInfosHandler: Finished updating profile infos for User ID {userId}");

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult<bool>> CompareLanguageId(CompareLanguageIdRequest request)
    {
        // EXTRACT REQUEST DATA
        var userId = request.UserId;
        var languageName = request.LanguageName;

        // GET USER
        var user = await _userManager.FindByIdAsync(userId);

        // GET LANGUAGE ID BY NAME
        var languageId = LanguageMapper.FromName(languageName);

        if (user!.NativeLanguageId == (int)languageId!)
            return ServiceResult<bool>.Success(true, HttpStatusCode.OK);

        return ServiceResult<bool>.Success(false, HttpStatusCode.OK);
    }
}