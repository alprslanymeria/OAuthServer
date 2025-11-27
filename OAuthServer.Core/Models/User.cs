using Microsoft.AspNetCore.Identity;

namespace OAuthServer.Core.Models;

public class User : IdentityUser
{
    public string? ImageUrl { get; set; }
    public int? NativeLanguageId { get; set; }

    // REFERANS ALDIKLARI (PARENT'LARI)
    public Language? Language { get; set; } // FOR NativeLanguageId


    // REFERANS VERDİKLERİ (CHILD'LARI)
    public ICollection<Flashcard> Flashcards { get; set; } = [];
    public ICollection<Listening> Listenings { get; set; } = [];
    public ICollection<Writing> Writings { get; set; } = [];
    public ICollection<Reading> Readings { get; set; } = [];
}
