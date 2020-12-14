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
    public class MembershipUpdateModel : PageModel
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
        public Models.Membership MembershipRecords { get; set; }

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

            MembershipRecords = new Models.Membership();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Membership WHERE MembershipId = @MembershipId";

                command.Parameters.AddWithValue("@MembershipId", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MembershipRecords.MembershipID = reader.GetInt32(0);
                    MembershipRecords.MembershipName = reader.GetString(1);
                    MembershipRecords.MembershipPrice = (float)reader.GetDouble(2);
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
                command.CommandText = "UPDATE Membership SET Membership = @Membership, Price = @Price WHERE MembershipId = @MembershipId";

                command.Parameters.AddWithValue("@MembershipId", MembershipRecords.MembershipID);
                command.Parameters.AddWithValue("@Membership", MembershipRecords.MembershipName);
                command.Parameters.AddWithValue("@Price", (float)MembershipRecords.MembershipPrice);

                command.ExecuteNonQuery();

            }
            conn.Close();

            return RedirectToPage("/Admins/AdminView");
        }
    }
}
