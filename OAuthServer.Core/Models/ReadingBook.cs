using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class ReadingBook
    {
        public int Id { get; set; }
        public int ReadingId { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string LeftColor { get; set; } = null!;
        public string SourceUrl { get; set; } = null!;

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required Reading Reading { get; set; } // FOR ReadingId

        // REFERANS VERDİKLERİ (CHILD'LARI)
        public ICollection<ReadingOldSession> ReadingOldSessions { get; set; } = [];
    }
}
