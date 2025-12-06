namespace OAuthServer.Core.DTOs;

// KULLANICIYA SIGN UP İŞLEMİNDEN SONRA DÖNÜLEN DTO

public record UserDto(
    
    string Id,
    string? UserName,
    string Email,
    string? ImageUrl,
    int NativeLanguageId);