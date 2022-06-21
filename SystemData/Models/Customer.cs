using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemData.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bills>();
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhonNumber1 { get; set; }
        public string PhonNumber2 { get; set; }

        public string Password { get; set; }

        public string IdentityNumber { get; set; }

        public int CustomerIdentityId { get; set; }

        public int? UserId { get; set; }

        public int CustomerTypeId { get; set; }
        public int CustomerJobsId { get; set; }
        public CustomerJobs CustomerJobsNavigation { get; set; }
        public CustomerType CustomerType { get; set; }


        public CustomerIdentity Identity { get; set; }
        public ICollection<Bills> Bills { get; set; }
    }
}