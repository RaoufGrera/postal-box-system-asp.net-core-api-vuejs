using System;
using System.Collections.Generic;
using System.Text;

namespace SystemData.Models
{
    public class NoteTypeBills
    {
        public int Id { get; set; }
        public int NoteTypeId { get; set; }
        public virtual NoteType NoteType { get; set; }

        public DateTime CreateDate { get; set; }
        public int BillsId { get; set; }
        public virtual Bills Bills { get; set; }
        public bool  IsRead { get; set; }
        public DateTime? ReadDate { get; set; }


    }
}
