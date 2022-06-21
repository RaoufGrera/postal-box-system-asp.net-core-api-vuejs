using System;
using System.Collections.Generic;

namespace SystemData.Models
{
    public partial class ExtraCost
    {
        public ExtraCost()
        {
            OtherDetailsRent = new HashSet<OtherDetailsRent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ServesCost { get; set; }

        public ICollection<OtherDetailsRent> OtherDetailsRent { get; set; }
    }
}
