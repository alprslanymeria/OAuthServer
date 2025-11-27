namespace OAuthServer.Core.DTOs;

// KULLANICIYA HATA BİLGİSİ DÖNMEK İÇİN KULLANILAN DTO
public class ErrorDto
{
    // PRIVATE SET YAPTIK ÇÜNKÜ PROPERTY'LER SADECE CTOR ÜZERİNDEN DEĞER ALABİLSİN
    // SADECE GET OLARAK BIRAKSAYDIK CTOR ÜZERİNDEN SET EDİLEBİLİRDİ AMA DİĞER METOTLARDAN ALAMAZDI.
    // GET + PRIVATE SET İLE BİRLİKTE CTOR DIŞINDAKİ METOTLARDAN DA SET EDİLEBİLİR.
    public List<String> Errors { get; private set; }
    public bool IsShow { get; private set; }

    public ErrorDto()
    {
        Errors = [];
    }

    public ErrorDto(string error, bool isShow)
    {
        Errors = [];
        Errors.Add(error);
        IsShow = isShow;
    }

    public ErrorDto(List<string> errors, bool isShow)
    {
        Errors = errors;
        IsShow = isShow;
    }
}