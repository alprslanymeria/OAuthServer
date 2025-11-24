using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class ReadingSessionRow
    {
        public int Id { get; set; }
        public string ReadingOldSessionId { get; set; } = null!;
        public string SelectedSentence { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public string AnswerTranslate { get; set; } = null!;
        public decimal Similarity { get; set; }

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required ReadingOldSession ReadingOldSession { get; set; } // FOR ReadingOldSessionId
    }
}
