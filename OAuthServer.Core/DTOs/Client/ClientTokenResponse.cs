namespace OAuthServer.Core.DTOs.Client;

public record ClientTokenResponse(
    
    string AccessToken, 
    DateTime AccessTokenExpiration);