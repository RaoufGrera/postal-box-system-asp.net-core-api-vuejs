

using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SystemData.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string PictureUrl { get; set; }

        
        public string LastName { get; set; }
        public long? FacebookId { get; set; }
        public int? OfficeId { get; set; }

        public string PhoneAuth { get; set; }


        public virtual Office Office { get; set; }

        public string Name { get; set; }

        [DefaultValue(true)] public bool IsAdmin { get; set; }
        [DefaultValue(false)] public bool IsCustomer { get; set; }

        [DefaultValue(true)] public bool IsValid { get; set; }


        [DefaultValue(false)] public bool IsDelete { get; set; }

        public string Token { get; set; }

        [Required(ErrorMessage = "حقل {0} مطلوب")]
        [DisplayName("المدينة")]
        public string Password { get; set; }

    }
}