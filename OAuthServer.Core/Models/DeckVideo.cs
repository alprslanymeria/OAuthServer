using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.Models
{
    public class DeckVideo
    {
        public int Id { get; set; }
        public int ListeningCategoryId { get; set; }
        public string Correct { get; set; } = null!;
        public string SourceUrl { get; set; } = null!;

        // REFERANS ALDIKLARI (PARENT'LARI)
        public required ListeningCategory ListeningCategory { get; set; } // FOR ListeningCategoryId
    }
}
