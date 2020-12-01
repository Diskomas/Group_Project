using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class Member
    { 
   [Display(Name = "Member ID")]
    public int Memberid { get; set; } //Unique ID

    [Required]
    [Display(Name = "Membername")]
    public string MemberName { get; set; } // can be displayed throughout the website, e.g in index "Welcome Zairul"

    [Required]
    [Display(Name = "Email Address")]
    public string MemberEmail { get; set; } // For login 

    [Required]
    [Display(Name = "Card Number")]
    public string MemberCard { get; set; }

    [Required]
    [Display(Name = "Password")]
    public string MemberPassword { get; set; } // For login 

    [Required]
    [Display(Name = "Membership")]
    public string Membership { get; set; } // Free | Basic  £3.99 | Full £8.99 | *Only one can be selected*

        // Free - 1-2 Playlists 
        // Premium -
        // Full - all available playlists
        public string FileName { get; set; }

    }
}