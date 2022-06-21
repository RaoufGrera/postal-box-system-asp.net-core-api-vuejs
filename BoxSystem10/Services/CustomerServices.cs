using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemData;
using SystemData.Models;

namespace BoxSystem10.Services
{

    public interface ICustomerServices
    {

    }
    public class CustomerServices:ICustomerServices
    {
        private SystemContext _context;
        public CustomerServices(SystemContext context)
        {
            _context = context;
        }
        public async Task<Customer> GetAllCustomerByOfficeId(int OfficeId)
        {
            var result =  (from c in _context.Customer join j in _context.CustomerJobs on c.CustomerJobsId equals j.Id select c).FirstOrDefault();
                


              

            return result;
        }
    }
}
