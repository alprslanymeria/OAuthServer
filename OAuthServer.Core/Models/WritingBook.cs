using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class WritingBook
    {
        public int Id { get; set; }
        public int WritingId { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string LeftColor { get; set; } = null!;
        public string SourceUrl { get; set; } = null!;

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required Writing Writing { get; set; } // FOR WritingId

        // REFERANS VERDİKLERİ (CHILD'LARI)
        public ICollection<WritingOldSession> WritingOldSessions { get; set; } = [];
    }
}
