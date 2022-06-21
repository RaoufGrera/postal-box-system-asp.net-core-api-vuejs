using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoxSystem10.Model
{
    public class VUser
    {
        public int UserId;

        public string Name { get; set; }

        
        public string UserName { get; set; }
        public string OfficeName { get; set; }

        [Required(ErrorMessage = "حقل {0} مطلوب")]
        [DisplayName("المدينة")]
        public string Password { get; set; }
        
                    public string PhoneNumber { get; set; }

        public int? OfficeId { get; set; }
        public bool IsCustomer { get; set; }


        
        public bool IsAdmin { get; set; }

        public string Message { get; set; }
        public string Token { get; set; }

    }

 
}
