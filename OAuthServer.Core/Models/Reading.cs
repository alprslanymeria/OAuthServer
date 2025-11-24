using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class Reading
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int LanguageId { get; set; }
        public int PracticeId { get; set; }

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required User User { get; set; } // FOR UserId
        public required Language Language { get; set; } // FOR LanguageId
        public required Practice Practice { get; set; } // FOR PracticeId

        // REFERANS VERDİKLERİ (CHILD'LARI)
        public ICollection<ReadingBook> ReadingBooks = [];
        public ICollection<ReadingOldSession> ReadingOldSessions = [];
    }
}
