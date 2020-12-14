using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Admins
{
    public class UserUpdateModel : PageModel
    {
        // --------------------- VALIDATE SESSION ---------------------
        public string LoginAdminemail;
        public const string SessionKeyName1 = "LoginAdminemail";


        public string LoginAdminusername;
        public const string SessionKeyName2 = "LoginAdminusername";

        public string SessionID;
        public const string SessionKeyName3 = "AdminsessionID";
        // --------------------- VALIDATE SESSION ---------------------

        [BindProperty]
        public Models.Users UserRecords { get; set; }

        public IActionResult OnGet(int? id)
        {
            // --------------------- VALIDATE SESSION ---------------------
            LoginAdminemail = HttpContext.Session.GetString(SessionKeyName1);
            LoginAdminusername = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);

            if (string.IsNullOrEmpty(LoginAdminemail) && string.IsNullOrEmpty(LoginAdminusername) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Index");
            }
            // --------------------- VALIDATE SESSION ---------------------

            DatabaseConnection.Database_Connection dbstring = new DatabaseConnection.Database_Connection();
            string DbConnection = dbstring.DatabaseString();

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();


            UserRecords = new Models.Users();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Userz WHERE UserId = @MemberID";

                command.Parameters.AddWithValue("@MemberID", id);
                Console.WriteLine("The id : " + id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserRecords.MemberID = reader.GetInt32(0);
                    UserRecords.UserName = reader.GetString(1);
                    UserRecords.UserEmail = reader.GetString(2);
                    UserRecords.UserCard = reader.GetString(3);
                    UserRecords.UserPassword = reader.GetString(4);
                    UserRecords.MembershipId = reader.GetInt32(5);
                }

            }

            conn.Close();

            return Page();

        }

        public IActionResult OnPost()
        {
            DatabaseConnection.Database_Connection dbstring = new DatabaseConnection.Database_Connection();
            string DbConnection = dbstring.DatabaseString();

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Userz SET UserName = @UserName, UserEmail = @UserEmail, UserCardNumber = @UserCard, UserPassword = @UserPassword, MembershipId = @MembershipId WHERE UserId = @MemberID";

                command.Parameters.AddWithValue("@MemberID", UserRecords.MemberID);
                command.Parameters.AddWithValue("@UserName", UserRecords.UserName);
                command.Parameters.AddWithValue("@UserEmail", UserRecords.UserEmail);
                command.Parameters.AddWithValue("@UserCard", UserRecords.UserCard);
                command.Parameters.AddWithValue("@UserPassword", UserRecords.UserPassword);
                command.Parameters.AddWithValue("@MembershipId", UserRecords.MembershipId);
                if (UserRecords.MembershipId > 2 || UserRecords.MembershipId < 1)
                {
                    return Page();
                }
                else
                {
                    command.ExecuteNonQuery();
                }
                
            }

            conn.Close();

            return RedirectToPage("/Admins/AdminView");
        }


    }
}
