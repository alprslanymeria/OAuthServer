namespace OAuthServer.Core.Models;

public class DeckWord
{
    public int Id { get; set; }
    public int FlashcardCategoryId { get; set; }
    public string Question { get; set; } = default!;
    public string Answer { get; set; } = default!;

    // REFERANS ALDIKLARI (PARENT'LARI)
    public required FlashcardCategory FlashcardCategory { get; set; } // FOR FlashcardCategoryId
}