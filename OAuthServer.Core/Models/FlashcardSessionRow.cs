using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class FlashcardSessionRow
    {
        public int Id { get; set; }
        public string FlashcardOldSessionId { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public Boolean Status { get; set; }

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required FlashcardOldSession FlashcardOldSession { get; set; } // FOR FlashcardOldSessionId
    }
}
