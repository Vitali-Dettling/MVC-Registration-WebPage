using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homepage.ViewModels
{
    public class Login
    {
        [Required]
        [EmailAddress]
        [JsonProperty("Email")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("Password")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}
