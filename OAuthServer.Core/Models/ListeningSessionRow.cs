using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class ListeningSessionRow
    {
        public int Id { get; set; }
        public string ListeningOldSessionId { get; set; } = null!;
        public string ListenedSentence { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public decimal Similarity { get; set; }

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required ListeningOldSession ListeningOldSession { get; set; } // FOR ListeningOldSessionId
    }
}
