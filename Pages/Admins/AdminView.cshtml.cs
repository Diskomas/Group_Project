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
    public class AdminViewModel : PageModel
    {
        // --------------------- VALIDATE SESSION ---------------------
        public string LoginAdminemail;
        public const string SessionKeyName1 = "LoginAdminemail";


        public string LoginAdminusername;
        public const string SessionKeyName2 = "LoginAdminusername";

        public string SessionID;
        public const string SessionKeyName3 = "AdminsessionID";
        // --------------------- VALIDATE SESSION ---------------------

        public string Usermembership { get; set; }
        public List<Models.Admins> AdminRecord { get; set; }
        public List<Models.Users> UserRecord { get; set; }
        public List<Models.Membership> MemberRecord { get; set; }
       
        //--------- SEARCH DATABASE RECORDS ---------
        [BindProperty(SupportsGet = true)]
        public string SearchAdminRecord { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearcUserRecord { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Membershipnumber { get; set; }

        public List<int> MembershipnumberItems { get; set; } = new List<int> { 1, 2 };

        //--------- SEARCH DATABASE RECORDS ---------
        public IActionResult OnGet()
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


            // --------------------- CALL INFRO FROM DB ---------------------
            DatabaseConnection.Database_Connection dbstring = new DatabaseConnection.Database_Connection();
            string DbConnection = dbstring.DatabaseString();

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            // ------ ADMIN TABLE ------
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Admin";

                if (string.IsNullOrEmpty(SearchAdminRecord))
                {
                    // we just want to skip filtering here + this could be improved
                }
                else
                {
                    command.CommandText += " WHERE AdminUsername = @AdminName";
                    command.Parameters.AddWithValue("@AdminName", SearchAdminRecord);
                }

                SqlDataReader reader = command.ExecuteReader();

                AdminRecord = new List<Models.Admins>();

                while (reader.Read())
                {
                    Models.Admins record = new Models.Admins();
                    record.AdminID = reader.GetInt32(0);
                    record.AdminUserName = reader.GetString(1);
                    record.AdminEmail = reader.GetString(2);
                    record.AdminPassword = reader.GetString(3);

                    AdminRecord.Add(record);
                }

                reader.Close();
            }
            // ------ ADMIN TABLE ------

            // ------ USER TABLE ------
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Userz";

                if (string.IsNullOrEmpty(SearcUserRecord))
                {
                    // we just want to skip filtering here + this could be improved
                }
                else
                {
                    command.CommandText += " WHERE UserName = @UserName";
                    command.Parameters.AddWithValue("@UserName", SearcUserRecord);
                }

                SqlDataReader reader = command.ExecuteReader();

                UserRecord = new List<Models.Users>();

                while (reader.Read())
                {
                    Models.Users record = new Models.Users();
                    record.MemberID = reader.GetInt32(0);
                    record.UserName = reader.GetString(1);
                    record.UserEmail = reader.GetString(2);
                    record.UserCard = reader.GetString(3);
                    record.UserPassword = reader.GetString(4);
                    record.MembershipId = reader.GetInt32(5);

                    UserRecord.Add(record);
                }

                reader.Close();
            }
            // ------ USER TABLE ------

            // ------ SERVICES TABLE ------
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Membership";

                if (!string.IsNullOrEmpty(Membershipnumber))
                {
                    
                    if (Membershipnumber == "All")
                    {
                        // we just want to skip filtering here + this could be improved
                    }
                    else
                    {
                        command.CommandText += " WHERE MembershipId = @membersipID";
                        command.Parameters.AddWithValue("@membersipID", Convert.ToInt32(Membershipnumber));
                    }
                    
                }

                SqlDataReader reader = command.ExecuteReader();

                MemberRecord = new List<Models.Membership>();

                while (reader.Read())
                {
                    Models.Membership record = new Models.Membership();
                    record.MembershipID = reader.GetInt32(0);
                    record.MembershipName = reader.GetString(1);
                    record.MembershipPrice = (float)reader.GetDouble(2);

                    MemberRecord.Add(record);
                }
                
            }
            // ------ SERVICES TABLE ------

            return Page();
        }
    }
}
