using OAuthServer.Core.Services;

namespace OAuthServer.Core.DTOs.Extra;

public record UpdateProfileInfosRequest(

    string UserId,
    IFileUpload Image,
    string Name,
    int NativeLanguageId);