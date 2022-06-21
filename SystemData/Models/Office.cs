using System;
using System.Collections.Generic;
using System.Text;

namespace SystemData.Models
{
    public class Office
    {
        public Office()
        {
            User = new HashSet<AppUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

        public ICollection<AppUser> User { get; set; }

    }
}
