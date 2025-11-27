using OAuthServer.Core.DTOs;
using System.Text.Json.Serialization;

namespace OAuthServer.Core.Helper;

public class Response<T> where T : class
{
    public T? Data { get; private set; }
    public int StatusCode { get; private set; }
    public ErrorDto? Error { get; private set; }

    // BUNU KENDİ İÇ YAPIMIZDA KISA YOLDAN İŞLEMİN BAŞARILI OLUP OLMADIĞINI KONTROL ETMEK İÇİN KULLANACAĞIZ.
    [JsonIgnore]
    public bool IsSuccessful { get; private set; }


    // SUCCESS AND DATA TO RETURN
    public static Response<T> Success(T data, int statusCode) => new() { Data = data, StatusCode = statusCode, IsSuccessful = true };

    // SUCCESS BUT NO DATA TO RETURN
    public static Response<T> Success(int statusCode) => new() { Data = default, StatusCode = statusCode, IsSuccessful = true };

    // FAIL
    public static Response<T> Fail(ErrorDto errorDto, int statusCode) => new() { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };

    // FAIL BUT ONLY ONE ERROR MESSAGE
    public static Response<T> Fail(string errorMessage, bool isShow, int statusCode) => new() { Error = new ErrorDto(errorMessage, isShow), StatusCode = statusCode, IsSuccessful = false };
}
