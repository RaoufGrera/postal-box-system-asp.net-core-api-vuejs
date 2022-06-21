using System;
using System.Collections.Generic;

namespace SystemData.Models
{
    public partial class Boxes
    {
        public Boxes()
        {
            Bills = new HashSet<Bills>();
        }

        public int Id { get; set; }
        public int TypeBoxId { get; set; }
        public long NumberBox { get; set; }
        public bool IsUsed { get; set; }
        public bool State { get; set; }
        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }

        public TypeBox TypeBox { get; set; }
        public ICollection<Bills> Bills { get; set; }
    }
}
