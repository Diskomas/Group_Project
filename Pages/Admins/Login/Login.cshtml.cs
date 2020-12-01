using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Models;
using Group_Project.Pages.DatabaseConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Admins.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Admin Admin { get; set; }
        public string Message { get; set; }

        public string SessionID;


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
          /*if (!ModelState.IsValid)
            {
                return Page();
            }*/


            Database_Connection dbstring = new Database_Connection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            //   Console.WriteLine(User.UserName);
            Console.WriteLine(Admin.AdminEmail);
            Console.WriteLine(Admin.AdminPassword);

            //   Console.WriteLine(User.Userid);
            //  "SELECT FirstName, UserName, UserRole FROM UserTable WHERE UserName = @UName AND UserPassword = @Pwd";
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT AdminUsername, AdminEmail FROM Admin WHERE AdminEmail = @AEmail AND AdminPasword = @Pwd";
                //@"SELECT FirstName, UserName, UserRole FROM UserTable WHERE UserName = @UName AND UserPassword = @Pwd";
                //command.CommandText = "UPDATE Admin SET AdminUsername = @AdminUserName, AdminEmail = @AdminEmail, AdminPasword = @AdminPassword WHERE AdminId = @AdminID";
                command.Parameters.AddWithValue("@AEmail", Admin.AdminEmail);
                command.Parameters.AddWithValue("@Pwd", Admin.AdminPassword);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // User.FirstName = reader.GetString(0);
                    Admin.AdminUserName = reader.GetString(0);
                    Admin.AdminEmail = reader.GetString(1);

                    //  User.Role = reader.GetString(2);
                }


            }

            //   if (!string.IsNullOrEmpty(Admin.AdminEmail))
              if (!string.IsNullOrEmpty(Admin.AdminUserName))
            {
                SessionID = HttpContext.Session.Id;
                HttpContext.Session.SetString("sessionID", SessionID);
                HttpContext.Session.SetString("adminemail", Admin.AdminEmail);
                HttpContext.Session.SetString("adminusername", Admin.AdminUserName);
                //  return RedirectToPage("Users/UserPages/Free");

                return RedirectToPage("/Admins/AdminPages/AdminIndex");



            }
            else
            {
                Message = "Invalid Email and Password!";
                return Page();
            }



        }


    }
}