namespace OAuthServer.Core.Models;

public class Language
{
    public int Id { get; set; }
    public string Name { get; set; } = default!; // THIS PROPERTY SEEMS LIKE NULL FOR NOW, BUT EF WILL FILL IT AT RUNTIME; DON'T WORRY ABOUT THE WARNING.
    public string? ImageUrl { get; set; }

    // REFERANS VERDİKLERİ (CHILD'LARI)
    public ICollection<User> Users { get; set; } = [];
    public ICollection<Practice> Practices { get; set; } = [];
    public ICollection<Flashcard> Flashcards { get; set; } = [];
    public ICollection<Listening> Listenings { get; set; } = [];
    public ICollection<Writing> Writings { get; set; } = [];
    public ICollection<Reading> Readings { get; set; } = [];
}

