namespace OAuthServer.Core.Models;

public class ListeningSessionRow
{
    public int Id { get; set; }
    public string ListeningOldSessionId { get; set; } = default!;
    public string ListenedSentence { get; set; } = default!;
    public string Answer { get; set; } = default!;
    public decimal Similarity { get; set; }

    // REFERANS ALDIKLARI (PARENT'LARI)
    public required ListeningOldSession ListeningOldSession { get; set; } // FOR ListeningOldSessionId
}
