namespace OAuthServer.Core.DTOs.RefreshToken;

public record TokenResponse(
    
    string AccessToken,
    DateTime AccessTokenExpiration,
    string RefreshToken,
    DateTime RefreshTokenExpiration);
