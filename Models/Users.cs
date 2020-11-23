using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class Users
    {
        [Display(Name = "Member ID")]
        public int MemberID { get; set; } //Unique ID

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } // can be displayed throughout the website, e.g in index "Welcome Zairul"

        [Required]
        [Display(Name = "Email Address")]
        public string UserEmail { get; set; } // For login 

        [Required]
        [Display(Name = "Card Number")]
        public string UserCard { get; set; } // optional in database since there is a free option, if can't remove required i will make the database require it

        [Required]
        [Display(Name = "Password")]
        public string UserPassword { get; set; } // For login 

        [Required]
        [Display(Name = "Membership")]
        public string MembershipId { get; set; } // Free | Basic  £3.99 | Full £8.99 | *Only one can be selected*

                                               // Free - 1-2 Playlists 
                                               // Basic - 3-5 Playlists
                                               // Full - all available playlists


    }
}
