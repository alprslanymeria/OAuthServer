using OAuthServer.Core.Configuration;
using OAuthServer.Core.DTOs.Client;
using OAuthServer.Core.DTOs.RefreshToken;
using OAuthServer.Core.Models;

namespace OAuthServer.Core.Services;

public interface ITokenService
{
    // THE METHODS IN THIS INTERFACE ARE IMPLEMENTED IN THE SERVICE LAYER.
    // THE METHODS IN THIS INTERFACE CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.
    // THE DATA RETURNED FROM THE METHODS CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.

    TokenResponse CreateToken(User user);
    ClientTokenResponse CreateTokenByClient(Client client);
}