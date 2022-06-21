using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SystemData.Models
{
    public partial class Bills
    {
        public Bills()
        {
            OtherDetailsRent = new HashSet<OtherDetailsRent>();
        }

        public int Id { get; set; }
        public decimal TotalBills { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime ExpiretDate { get; set; }

        public DateTime? BookedExpret { get; set; }

        public DateTime DateBills { get; set; }
        public int? BillsNumber { get; set; }
        public int? DateStartRent { get; set; }
        public bool IsManual { get; set; }
        [DefaultValue("true")]  public bool IsActive { get; set; }
        public int PayTypeId { get; set; }

        public decimal? PriceKey { get; set; }


        public virtual PayType PayType { get; set; }
        public virtual AppUser AppUser { get; set; }

        public int? AppUserId { get; set; }

        public virtual Boxes Boxes { get; set; }
        public int BoxId { get; set; }

        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
        [DefaultValue("false")] public bool IsBooked { get; set; }
      //  [DefaultValue("false")]  public int IsOnline { get; set; }

      //  public int ExpiretDate { get; set; }




        public virtual ICollection<OtherDetailsRent> OtherDetailsRent { get; set; }
    }
}
