using System;
using System.Collections.Generic;

namespace SystemData.Models
{
    public partial class CustomerType
    {
        public CustomerType()
        {
            Customer = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Customer> Customer { get; set; }
    }
}
