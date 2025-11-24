using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class FlashcardCategory
    {
        public int Id { get; set; }
        public int FlashcardId { get; set; }
        public string Name { get; set; } = null!;

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required Flashcard Flashcard { get; set; } // FOR FlashcardId

        // REFERANS VERDİKLERİ (CHILD'LARI)
        public ICollection<FlashcardOldSession> FlashcardOldSessions { get; set; } = [];
        public ICollection<DeckWord> DeckWords { get; set; } = [];
    }
}
