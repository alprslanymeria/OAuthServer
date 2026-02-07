namespace OAuthServer.Core.DTOs.User;

public record SignUpRequest(
    
    string? UserName,
    string Email,
    string Password,
    int NativeLanguageId);