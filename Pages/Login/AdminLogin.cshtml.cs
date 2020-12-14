using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Login
{
    public class AdminLoginModel : PageModel
    {

        [BindProperty]
        public Models.Admins Admin { get; set; }

        public string Message { get; set; }

        public string SessionID;
        public const string SessionKeyName3 = "AdminsessionID";
        public const string SessionKeyName4 = "sessionID";
        public IActionResult OnPost()
        {
            DatabaseConnection.Database_Connection dbstring = new DatabaseConnection.Database_Connection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT AdminUsername, AdminEmail, AdminPasword FROM Admin WHERE AdminEmail = @AEmail AND AdminPasword = @APassword";


                // ---------------- VALIDATE IF FIELD CONTAINS DATA ----------------
                if (string.IsNullOrEmpty(Admin.AdminPassword) || string.IsNullOrEmpty(Admin.AdminEmail))
                {
                    command.Parameters.AddWithValue("@AEmail", "randomvalue123");
                    command.Parameters.AddWithValue("@APassword", "randomvalue123");
                }
                else
                {
                    command.Parameters.AddWithValue("@AEmail", Admin.AdminEmail);
                    command.Parameters.AddWithValue("@APassword", Admin.AdminPassword);
                }
                // ---------------- VALIDATE IF FIELD CONTAINS DATA ----------------


                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Admin.AdminUserName = reader.GetString(0);
                    Admin.AdminEmail = reader.GetString(1);
                }
            }

            if (!string.IsNullOrEmpty(Admin.AdminUserName))
            {
                SessionID = HttpContext.Session.Id;
                HttpContext.Session.SetString("AdminsessionID", SessionID);
                HttpContext.Session.SetString("LoginAdminemail", Admin.AdminEmail);
                HttpContext.Session.SetString("LoginAdminusername", Admin.AdminUserName);

                return RedirectToPage("/users/UserAccount");


            }
            else
            {
                Message = "Invalid Username or Password!";
                return Page();
            }


        }

        public IActionResult OnGet()
        {
            // --------------------- VALIDATE SESSION ---------------------
            SessionID = HttpContext.Session.GetString(SessionKeyName4);
            if (!string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/users/UserAccount");
            }
            else
            {
                SessionID = HttpContext.Session.GetString(SessionKeyName3);

                if (!string.IsNullOrEmpty(SessionID))
                {
                    return RedirectToPage("/Admins/AdminView");
                }  
            }
            // --------------------- VALIDATE SESSION ---------------------
            return Page();
        }
    }
}
