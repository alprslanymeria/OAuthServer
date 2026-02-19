using OAuthServer.Core.DTOs.Extra;
using OAuthServer.Core.DTOs.User;
using OAuthServer.Core.Helper;

namespace OAuthServer.Core.Services;

public interface IUserService
{
    // THE METHODS IN THIS INTERFACE ARE IMPLEMENTED IN THE SERVICE LAYER.
    // THE METHODS IN THIS INTERFACE CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.
    // THE DATA RETURNED FROM THE METHODS CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.

    Task<ServiceResult<UserDto>> CreateUserAsync(SignUpRequest request);
    Task<ServiceResult> UpdateProfileInfos(UpdateProfileInfosRequest request);
    Task<ServiceResult<UserDto>> GetProfileInfos(string userId);
    Task<ServiceResult<bool>> CompareLanguageId(CompareLanguageIdRequest request);
}