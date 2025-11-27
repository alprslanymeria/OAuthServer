namespace OAuthServer.Core.Models;

public class DeckVideo
{
    public int Id { get; set; }
    public int ListeningCategoryId { get; set; }
    public string Correct { get; set; } = default!;
    public string SourceUrl { get; set; } = default!;

    // REFERANS ALDIKLARI (PARENT'LARI)
    public required ListeningCategory ListeningCategory { get; set; } // FOR ListeningCategoryId
}