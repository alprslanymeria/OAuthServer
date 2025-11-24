using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Service.Services
{
    public static class SignService
    {
        // TOKEN İMZALAMA İŞLEMLERİNDE KULLANMAK İÇİN SYMETRIC KEY OLUŞTURUYORUZ.
        public static SecurityKey GetSymetricKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
