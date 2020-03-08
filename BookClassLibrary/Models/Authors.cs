using System;
using System.Collections.Generic;

namespace BookClassLibrary.Models
{
    public partial class Authors
    {
        public Authors()
        {
            AuthorIsbn = new HashSet<AuthorIsbn>();
        }

        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<AuthorIsbn> AuthorIsbn { get; set; }
    }
}
