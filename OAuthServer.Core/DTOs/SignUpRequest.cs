namespace OAuthServer.Core.DTOs;

// KULLANICIDAN SIGN UP SIRASINDA ALINAN DTO

public record SignUpRequest(
    
    string? UserName,
    string Email,
    string Password,
    int NativeLanguageId);