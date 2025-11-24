using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class DeckWord
    {
        public int Id { get; set; }
        public int FlashcardCategoryId { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required FlashcardCategory FlashcardCategory { get; set; } // FOR FlashcardCategoryId
    }
}
