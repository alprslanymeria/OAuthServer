namespace OAuthServer.Core.DTOs;

// KULLANICIDAN SIGN UP SIRASINDA ALINAN DTO

public record SignUpRequest(
    
    string? Username,
    string Email,
    string Password);