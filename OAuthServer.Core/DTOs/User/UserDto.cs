namespace OAuthServer.Core.DTOs.User;

public record UserDto(
    
    string Id,
    string? UserName,
    string Email,
    string? ImageUrl,
    int NativeLanguageId);