namespace OAuthServer.Core.DTOs;

// BU TOKENDTO CLASS'I ÜYELİK SİSTEMİ İÇEREN API'LERE İSTEK YAPACAK CLIENT'LARA DÖNÜLÜR.
// EĞER CLIENT, ÜYELİK SİSTEMİ İÇERMEYEN BİR API'E İSTEK YAPACAK İSE CLIENT TOKEN DTO KULLANIR.
// BİZİM SENARYOMUZDA API'LER BU PROJE İÇİNDE OLACAK ÇÜNKÜ DEPLOY EDERKEN MALİYETTEN KAÇINMAK İÇİN.
// E DOĞAL OLARAK ÜYELİK SİSTEMİ İÇERMİŞ OLACAK.

// KULLANICIYA TOKEN BİLGİSİ DÖNMEK İÇİN KULLANILAN DTO

public record TokenResponse(
    
    string AccessToken,
    DateTime AccessTokenExpiration,
    string RefreshToken,
    DateTime RefreshTokenExpiration);


//public class ClientTokenDto
//{
//    public string AccessToken { get; set; } = null!;
//    public DateTime AccessTokenExpiration { get; set; }
//}