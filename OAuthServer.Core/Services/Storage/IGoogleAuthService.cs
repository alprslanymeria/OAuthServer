using OAuthServer.Core.DTOs.GoogleAuth;
using OAuthServer.Core.DTOs.RefreshToken;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace OAuthServer.Core.Services.Storage;

public interface IGoogleAuthService
{
    // THE METHODS IN THIS INTERFACE ARE IMPLEMENTED IN THE SERVICE LAYER.
    // THE METHODS IN THIS INTERFACE CAN BE USED IN THE SERVICE OR PRESENTATION (API) LAYER.

    void ValidateRedirectUri(string redirectUri);
    GoogleUserInfo ExtractUserInfo(ClaimsPrincipal principal);
    string BuildTokenRedirectUrl(string redirectUri, TokenResponse token);
}
