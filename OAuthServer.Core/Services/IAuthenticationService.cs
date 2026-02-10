using OAuthServer.Core.DTOs.Client;
using OAuthServer.Core.DTOs.RefreshToken;
using OAuthServer.Core.DTOs.User;
using OAuthServer.Core.Helper;

namespace OAuthServer.Core.Services;

public interface IAuthenticationService
{
    // THE METHODS IN THIS INTERFACE ARE IMPLEMENTED IN THE SERVICE LAYER.
    // THE METHODS IN THIS INTERFACE CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.
    // THE DATA RETURNED FROM THE METHODS CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.

    Task<ServiceResult<TokenResponse>> CreateTokenAsync(SignInRequest request);
    Task<ServiceResult<TokenResponse>> CreateTokenByRefreshToken(string refreshToken);
    Task<ServiceResult> RevokeRefreshToken(string refreshToken);
    Task<ServiceResult<ClientTokenResponse>> CreateTokenByClient(ClientSignInRequest request);
    Task<ServiceResult<TokenResponse>> CreateTokenByExternalLogin(string email, string? name, string googleSubjectId, string? picture);
}