namespace OAuthServer.Core.DTOs.User;

public record UserDto(
    
    string Id,
    string? UserName,
    string Email,
    string? Image,
    int NativeLanguageId);