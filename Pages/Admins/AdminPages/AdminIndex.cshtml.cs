using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Admins.AdminPages
{
    public class AdminIndexModel : PageModel
    {
        public string AdminUserName;
        public const string SessionKeyName4 = "adminusername";


        public string AdminEmail;
        public const string SessionKeyName5 = "adminemail";

        public string SessionID;
        public const string SessionKeyName6 = "sessionID";


        public IActionResult OnGet()
        {
            AdminUserName = HttpContext.Session.GetString(SessionKeyName4);
            AdminEmail = HttpContext.Session.GetString(SessionKeyName5);
            SessionID = HttpContext.Session.GetString(SessionKeyName6);

            if (string.IsNullOrEmpty(AdminUserName) && string.IsNullOrEmpty(AdminEmail) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("Admins/Login/Login");
            }
            return Page();

        }
    }
}