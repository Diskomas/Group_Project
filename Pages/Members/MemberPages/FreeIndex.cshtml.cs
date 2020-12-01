using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Members.MemberPages
{
    public class FreeIndexModel : PageModel
    {
        /*HttpContext.Session.SetString("sessionID", SessionID);
                HttpContext.Session.SetString("memberemail", Member.MemberEmail);
                HttpContext.Session.SetString("membername", Member.MemberName);
         */
        public string MemberName;
        public const string SessionKeyName1 = "membername";


        public string MemberEmail;
        public const string SessionKeyName2 = "membermail";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";


        public IActionResult OnGet()
        {
            MemberName = HttpContext.Session.GetString(SessionKeyName1);
            MemberEmail = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);

            if (string.IsNullOrEmpty(MemberName) && string.IsNullOrEmpty(MemberEmail) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Members/MemberPages/Login/Login");
            }
            return Page();

        }
    }
}
