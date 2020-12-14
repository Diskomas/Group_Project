using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class Membership
    {
        [Display(Name = "Membership ID")]
        public int MembershipID { get; set; }

        [Display(Name = "Membership")]
        public string MembershipName { get; set; }

        [Display(Name = "Price")]
        public float MembershipPrice { get; set; }
    }
}
