namespace OAuthServer.Core.Models;

public class ReadingSessionRow
{
    public int Id { get; set; }
    public string ReadingOldSessionId { get; set; } = default!;
    public string SelectedSentence { get; set; } = default!;
    public string Answer { get; set; } = default!;
    public string AnswerTranslate { get; set; } = default!;
    public decimal Similarity { get; set; }

    // REFERANS ALDIKLARI (PARENT'LARI)
    public required ReadingOldSession ReadingOldSession { get; set; } // FOR ReadingOldSessionId
}