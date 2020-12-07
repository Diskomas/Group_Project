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
    public class UserLoginModel : PageModel
    {
        [BindProperty]
        public Models.Users User { get; set; }

        public string Message { get; set; }

        public string SessionID;

        public IActionResult OnPost()
        {
            DatabaseConnection.Database_Connection dbstring = new DatabaseConnection.Database_Connection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(User.UserName);
            Console.WriteLine(User.UserPassword);


            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT MembershipId, UserName, UserEmail, UserPassword FROM Userz WHERE UserEmail = @UEmail AND UserPassword = @UPassword";


                // ---------------- VALIDATE IF FIELD CONTAINS DATA ----------------
                if (string.IsNullOrEmpty(User.UserPassword) || string.IsNullOrEmpty(User.UserEmail))
                {
                    command.Parameters.AddWithValue("@UEmail", "randomvalue123");
                    command.Parameters.AddWithValue("@UPassword", "randomvalue123");
                }
                else
                {
                    command.Parameters.AddWithValue("@UEmail", User.UserEmail);
                    command.Parameters.AddWithValue("@UPassword", User.UserPassword);
                }
                // ---------------- VALIDATE IF FIELD CONTAINS DATA ----------------


                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User.MembershipId = reader.GetInt32(0);
                    User.UserName = reader.GetString(1);
                    User.UserEmail = reader.GetString(2);

                }
            }

            if (!string.IsNullOrEmpty(User.UserName))
            {
                SessionID = HttpContext.Session.Id;
                HttpContext.Session.SetString("sessionID", SessionID);
                HttpContext.Session.SetString("LoginUseremail", User.UserEmail);
                HttpContext.Session.SetString("LoginUserusername", User.UserName);
                HttpContext.Session.SetInt32("LoginUserid", User.MembershipId);

                if (User.MembershipId == 1)
                {
                    return RedirectToPage("/Users/free");
                }
                else
                {
                    
                    return RedirectToPage("/Users/premium");
                }

            }
            else
            {
                Message = "Invalid Username or Password!";
                return Page();
            }


        }
        public void OnGet()
        {
        }
    }
}
