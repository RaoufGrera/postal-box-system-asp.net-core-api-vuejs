using System;
using System.Collections.Generic;

namespace SystemData.Models
{
    public partial class TypeBox
    {
        public TypeBox()
        {
            Boxes = new HashSet<Boxes>();
            DetailsRent = new HashSet<DetailsRent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Boxes> Boxes { get; set; }
        public ICollection<DetailsRent> DetailsRent { get; set; }
    }
}
