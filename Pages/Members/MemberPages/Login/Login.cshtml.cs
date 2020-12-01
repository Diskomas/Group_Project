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

namespace Group_Project.Pages.Members.MemberPages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }
        public string Message { get; set; }

        public string SessionID;


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            /* if (!ModelState.IsValid)
              {
                  return Page();
              }*/

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
            Console.WriteLine(Member.MemberEmail);
            Console.WriteLine(Member.MemberPassword);

            //   Console.WriteLine(User.Userid);
            //  "SELECT FirstName, UserName, UserRole FROM UserTable WHERE UserName = @UName AND UserPassword = @Pwd";
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT MemberName, MemberEmail, Membership FROM Member WHERE MemberEmail = @MEmail AND MemberPassword = @Pwd";
                //@"SELECT FirstName, UserName, UserRole FROM UserTable WHERE UserName = @UName AND UserPassword = @Pwd";
                //command.CommandText = "UPDATE Admin SET AdminUsername = @AdminUserName, AdminEmail = @AdminEmail, AdminPasword = @AdminPassword WHERE AdminId = @AdminID";
                command.Parameters.AddWithValue("@MEmail", Member.MemberEmail);
                command.Parameters.AddWithValue("@Pwd", Member.MemberPassword);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // User.FirstName = reader.GetString(0);
                    Member.MemberName = reader.GetString(0);
                    Member.MemberEmail = reader.GetString(1);

                     Member.Membership = reader.GetString(2);
                }


            }
            //bug if you dont enter email
            if (!string.IsNullOrEmpty(Member.MemberName))
            {
                SessionID = HttpContext.Session.Id;
                HttpContext.Session.SetString("sessionID", SessionID);
                HttpContext.Session.SetString("memberemail", Member.MemberEmail);
                HttpContext.Session.SetString("membername", Member.MemberName);
                

                if (Member.Membership == "Free")
                {
                    return RedirectToPage("/Members/MemberPages/FreeIndex");
                }
                else
                {
                    return RedirectToPage("/Members/MemberPages/PremiumIndex");
                }
                //return RedirectToPage("/Members/MemberPages/MemberIndex");



            }
            else
            {
                Message = "Invalid Email and Password!";
                return Page();
            }



        }


    }
}