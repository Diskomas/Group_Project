using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Logout
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {

            HttpContext.Session.Clear();


        }
    }
}
