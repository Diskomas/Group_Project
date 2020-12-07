using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Users
{
    
    public class FreeModel : PageModel
    {
        // --------------------- VALIDATE SESSION ---------------------
        public string LoginUseremail;
        public const string SessionKeyName1 = "LoginUseremail";


        public string LoginUserusername;
        public const string SessionKeyName2 = "LoginUserusername";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";

        public int LoginUserMembership;
        public const string SessionKeyName4 = "LoginUserusername";

        // --------------------- VALIDATE SESSION ---------------------
        public IActionResult OnGet()
        {
            // --------------------- VALIDATE SESSION ---------------------
            LoginUseremail = HttpContext.Session.GetString(SessionKeyName1);
            LoginUserusername = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);

            if (string.IsNullOrEmpty(LoginUseremail) && string.IsNullOrEmpty(LoginUserusername) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Services/Memberships");
            }
            // --------------------- VALIDATE SESSION ---------------------

            return Page();
        }
    }
}
