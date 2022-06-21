using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SystemData.Models
{
    class JwtToken
    {
        [Key]
        public int Id { get; set; }
 
        public AppUser User { get; set; }

        public string Provider { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsValid { get; set; }

    }

}
