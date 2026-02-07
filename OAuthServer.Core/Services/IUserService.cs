using OAuthServer.Core.DTOs.Extra;
using OAuthServer.Core.DTOs.User;
using OAuthServer.Core.Helper;

namespace OAuthServer.Core.Services;

public interface IUserService
{
    // THE METHODS IN THIS INTERFACE ARE IMPLEMENTED IN THE SERVICE LAYER.
    // THE METHODS IN THIS INTERFACE CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.
    // THE DATA RETURNED FROM THE METHODS CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.

    Task<Response<UserDto>> CreateUserAsync(SignUpRequest request);
    Task<Response<UserDto>> GetUserByUserNameAsync(string username);

    // EXTRA METHODS FOR MY NEXTJS PROJECT
    Task<Response> UpdateProfileInfos(UpdateProfileInfosRequest request);
    Task<Response<UserDto>> GetProfileInfos(string userId);
    Task<Response<bool>> CompareLanguageId(CompareLanguageIdRequest request);
}