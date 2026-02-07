namespace OAuthServer.Core.DTOs.Client;

public record ClientSignInRequest(
    
    string ClientId,
    string ClientSecret);