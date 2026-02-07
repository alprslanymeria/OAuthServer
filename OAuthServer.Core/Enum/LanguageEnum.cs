namespace OAuthServer.Core.Enum;

public enum LanguageId
{
    english = 1,
    turkish = 2,
    german = 3,
    russian = 4
}

public static class LanguageMapper
{
    public static LanguageId? FromName(string name)
    {
        return name.ToLower() switch
        {
            "english" => (LanguageId?)LanguageId.english,
            "turkish" => (LanguageId?)LanguageId.turkish,
            "german" => (LanguageId?)LanguageId.german,
            "russian" => (LanguageId?)LanguageId.russian,
            _ => null,
        };
    }
}
