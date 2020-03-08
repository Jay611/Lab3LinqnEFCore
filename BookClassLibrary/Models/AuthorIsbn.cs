using System;
using System.Collections.Generic;

namespace BookClassLibrary.Models
{
    public partial class AuthorIsbn
    {
        public int AuthorId { get; set; }
        public string Isbn { get; set; }

        public virtual Authors Author { get; set; }
        public virtual Titles IsbnNavigation { get; set; }
    }
}
