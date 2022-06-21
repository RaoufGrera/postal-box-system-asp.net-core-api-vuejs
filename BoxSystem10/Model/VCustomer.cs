using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoxSystem10.Model
{
    public class VCustomer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhonNumber1 { get; set; }
        public string PhonNumber2 { get; set; }

        public string IdentityNumber { get; set; }


        public string CustomerTypeName { get; set; }

        public string CustomerJobName { get; set; }
        public int CustomerTypeId { get; set; }
        public int CustomerJobsId { get; set; }

        public int CustomerIdentityId { get; set; }

        

    }
}
