using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Users
{
    public class UserAccountModel : PageModel
    {
        // --------------------- VALIDATE SESSION FOR ADMIN ---------------------
        public string LoginAdminemail;
        public const string SessionKeyName1 = "LoginAdminemail";


        public string LoginAdminusername;
        public const string SessionKeyName2 = "LoginAdminusername";

        public string AdminSessionID;
        public const string SessionKeyName3 = "AdminsessionID";
        // --------------------- VALIDATE SESSION FOR ADMIN ---------------------

        // --------------------- VALIDATE SESSION FOR MEMBER---------------------
        public string LoginUseremail;
        public const string SessionKeyName4 = "LoginUseremail";


        public string LoginUserusername;
        public const string SessionKeyName5 = "LoginUserusername";

        public string UserSessionID;
        public const string SessionKeyName6 = "sessionID";

        public int LoginUserMembership;
        public const string SessionKeyName7 = "LoginUserid";
        // --------------------- VALIDATE SESSION FOR MEMBER---------------------

        [BindProperty]
        public Models.Users UserRecords { get; set; }

        [BindProperty]
        public Models.Admins AdminRecords { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Display_name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Display_email { get; set; }

        [BindProperty(SupportsGet = true)]
        public Boolean UserCheck { get; set; }
        public IActionResult OnGet()
        {
            // --------------------- VALIDATE SESSION ---------------------
            
            LoginAdminemail = HttpContext.Session.GetString(SessionKeyName1);
            LoginAdminusername = HttpContext.Session.GetString(SessionKeyName2);
            AdminSessionID = HttpContext.Session.GetString(SessionKeyName3);

            LoginUseremail = HttpContext.Session.GetString(SessionKeyName4);
            LoginUserusername = HttpContext.Session.GetString(SessionKeyName5);
            UserSessionID = HttpContext.Session.GetString(SessionKeyName6);

            if (!string.IsNullOrEmpty(LoginAdminemail) && !string.IsNullOrEmpty(LoginAdminusername) && !string.IsNullOrEmpty(AdminSessionID))
            {

                DatabaseConnection.Database_Connection dbstring = new DatabaseConnection.Database_Connection();
                string DbConnection = dbstring.DatabaseString();

                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                AdminRecords = new Models.Admins();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "SELECT * FROM Admin WHERE AdminEmail = @AdminEmail";

                    command.Parameters.AddWithValue("@AdminEmail", LoginAdminemail);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        AdminRecords.AdminID = reader.GetInt32(0);
                        AdminRecords.AdminUserName = reader.GetString(1);
                        AdminRecords.AdminEmail = reader.GetString(2);
                        AdminRecords.AdminPassword = reader.GetString(3);
                        AdminRecords.AdminReset = reader.GetString(4);
                    }

                }
                conn.Close();
                UserCheck = false;
                Display_name = AdminRecords.AdminUserName;
                Display_email = AdminRecords.AdminEmail;
                return Page();
            }
            else
            {
                if (string.IsNullOrEmpty(UserSessionID))
                {
                    return RedirectToPage("/login/UserLogin");
                }
                else
                {
                    LoginUserMembership = (int)HttpContext.Session.GetInt32(SessionKeyName7);

                    if (string.IsNullOrEmpty(LoginUseremail) && string.IsNullOrEmpty(LoginUserusername) && string.IsNullOrEmpty(UserSessionID))
                    {
                        return RedirectToPage("/login/UserLogin");
                    }
                    else
                    {
                        DatabaseConnection.Database_Connection dbstring = new DatabaseConnection.Database_Connection();
                        string DbConnection = dbstring.DatabaseString();

                        SqlConnection conn = new SqlConnection(DbConnection);
                        conn.Open();

                        UserRecords = new Models.Users();


                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = conn;
                            command.CommandText = "SELECT * FROM Userz WHERE UserEmail = @UserEmail";

                            command.Parameters.AddWithValue("@UserEmail", LoginUseremail);

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
                        UserCheck = true;
                        Display_name = UserRecords.UserName;
                        Display_email = UserRecords.UserEmail;
                        return Page();
                    }
                }
            }
           

            // --------------------- VALIDATE SESSION ---------------------

        }

        public IActionResult OnPost()
        {
            
            if (!string.IsNullOrEmpty(AdminSessionID))
            {
                @HttpContext.Session.Clear();
                return RedirectToPage("/login/AdminLogin");
            }
            else
            {
                @HttpContext.Session.Clear();
                return RedirectToPage("/login/UserLogin");
            }
            
        }
    }
}
