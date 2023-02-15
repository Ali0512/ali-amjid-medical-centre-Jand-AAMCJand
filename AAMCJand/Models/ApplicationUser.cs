using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AAMCJand.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Ful name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Address Required")]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }
    }
}
