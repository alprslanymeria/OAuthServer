using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class ListeningCategory
    {
        public int Id { get; set; }
        public int ListeningId { get; set; }
        public string Name { get; set; } = null!;

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required Listening Listening { get; set; } // FOR ListeningId

        // REFERANS VERDİKLERİ (CHILD'LARI)
        public ICollection<ListeningOldSession> ListeningOldSessions { get; set; } = [];
        public ICollection<DeckVideo> DeckVideos { get; set; } = [];
    }
}
