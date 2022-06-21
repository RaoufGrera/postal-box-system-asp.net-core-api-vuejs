using System;
using System.Collections.Generic;

namespace SystemData.Models
{
    public partial class OtherDetailsRent
    {
        public int Id { get; set; }
        public int BillsId { get; set; }
        public int ExtraCostId { get; set; }
        public DateTime InsertDate { get; set; }

        public Bills Bills { get; set; }
        public ExtraCost ExtraCost { get; set; }
    }
}
