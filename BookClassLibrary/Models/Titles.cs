using System;
using System.Collections.Generic;

namespace BookClassLibrary.Models
{
    public partial class Titles
    {
        public Titles()
        {
            AuthorIsbn = new HashSet<AuthorIsbn>();
        }

        public string Isbn { get; set; }
        public string Title { get; set; }
        public int EditionNumber { get; set; }
        public string Copyright { get; set; }

        public virtual ICollection<AuthorIsbn> AuthorIsbn { get; set; }
    }
}
