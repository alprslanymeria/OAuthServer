using OAuthServer.Core.DTOs;
using OAuthServer.Core.Helper;

namespace OAuthServer.Core.Services;

public interface IAuthenticationService
{
    // BURDA BULUNAN METOTLARIN IMPLAMENTASYONU SERVICE KATMANINDA YAPILIR.
    // BURADA BULUNAN METOTLAR SERVICE VEYA PRESENTATION (API) KATMANINDA KULLANILABİLİR.
    // METOTLARDAN DÖNEN VERİ İSE SERVICE VEYA PRESENTATION (API) KATMANINDA KULLANILABİLİR.

    Task<Response<TokenResponse>> CreateTokenAsync(SignInRequest request);

    Task<Response<TokenResponse>> CreateTokenByRefreshToken(string refreshToken);

    Task<Response> RevokeRefreshToken(string refreshToken);

    //Response<ClientTokenDto> CreateTokenByClient(ClientSignInDto clientSignInDto);
}