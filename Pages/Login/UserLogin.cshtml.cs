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

        public int LoginUserMembership;
        public const string SessionKeyName4 = "LoginUserid";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";
        public const string SessionKeyName5 = "AdminsessionID";
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
                    return RedirectToPage("/Users/UserAccount");
                }
                else
                {
                    
                    return RedirectToPage("/Users/UserAccount");
                }

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

            SessionID = HttpContext.Session.GetString(SessionKeyName5);

            if (!string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Users/UserAccount");
            }
            else
            {
                SessionID = HttpContext.Session.GetString(SessionKeyName3);

                if (!string.IsNullOrEmpty(SessionID))
                {

                    LoginUserMembership = (int)HttpContext.Session.GetInt32(SessionKeyName4);
                    if (LoginUserMembership == 2)
                    {
                        return RedirectToPage("/Users/Premium");
                    }
                    else if (LoginUserMembership == 1)
                    {
                        return RedirectToPage("/Users/Free");
                    }
                
                }
            }
            // --------------------- VALIDATE SESSION ---------------------

            return Page();
        }
    }
}
