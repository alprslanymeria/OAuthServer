using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class Practice
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; } = null!;

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required Language Language { get; set; } // FOR LanguageId

        // REFERANS VERDİKLERİ (CHILD'LARI)
        public ICollection<Flashcard> Flashcards { get; set; } = [];
        public ICollection<Listening> Listenings { get; set; } = [];
        public ICollection<Writing> Writings { get; set; } = [];
        public ICollection<Reading> Readings { get; set; } = [];
    }
}
