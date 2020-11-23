using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class Admins
    {
        [Display(Name = "Admin ID")]
        public int AdminID { get; set; } //Unique ID

        [Required]
        [Display(Name = "UserName")]
        public string AdminUserName { get; set; } // can be displayed throughout the website, e.g in index "Welsome Zairul"

        [Required]
        [Display(Name = "Email Address")]
        public string AdminEmail { get; set; } // For Login

        [Required]
        [Display(Name = "Password")]
        public string AdminPassword { get; set; } // For login 

    }
}
