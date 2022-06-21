using System;
using System.Collections.Generic;

namespace SystemData.Models
{
    public partial class DetailsCost
    {
        public DetailsCost()
        {
            DetailsRent = new HashSet<DetailsRent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DayFrom { get; set; }
        public int DayTo { get; set; }

        public ICollection<DetailsRent> DetailsRent { get; set; }
    }
}
