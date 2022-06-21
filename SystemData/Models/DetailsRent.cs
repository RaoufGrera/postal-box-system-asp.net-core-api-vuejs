using System;
using System.Collections.Generic;

namespace SystemData.Models
{
    public partial class DetailsRent
    {
        public int Id { get; set; }
        public int TypeBoxId { get; set; }
        public bool IsValid { get; set; }
        public bool IsValidDate { get; set; }
        public decimal Cost { get; set; }
        public int DetailsCostId { get; set; }

        public DetailsCost DetailsCost { get; set; }
        public TypeBox TypeBox { get; set; }
    }
}
