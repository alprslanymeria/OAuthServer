using OAuthServer.Core.DTOs;
using OAuthServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Services
{
    public interface ITokenService
    {
        // BURDA BULUNAN METOTLARIN IMPLAMENTASYONU SERVICE KATMANINDA YAPILIR.
        // BURADA BULUNAN METOTLAR SERVICE VEYA PRESENTATION (API) KATMANINDA KULLANILABİLİR.
        // METOTLARDAN DÖNEN VERİ İSE SERVICE VEYA PRESENTATION (API) KATMANINDA KULLANILABİLİR.

        TokenDto CreateToken(User user);

        //ClientTokenDto CreateTokenByClient(Client client)

        //public class Client
        //{
        //    public string Id { get; set; } = null!;
        //    public string Secret { get; set; } = null!;
        //    public List<string> Audiences { get; set; } = null!;
        //}
    }
}
