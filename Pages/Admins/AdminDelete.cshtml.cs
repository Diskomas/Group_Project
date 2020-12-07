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
    public class AdminDeleteModel : PageModel
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
        public Models.Admins AdminRecord { get; set; }

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

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Admin WHERE AdminId = @AdminID";
                command.Parameters.AddWithValue("@AdminID", id);

                SqlDataReader reader = command.ExecuteReader();
                AdminRecord = new Models.Admins();
                while (reader.Read())
                {
                    AdminRecord.AdminID = reader.GetInt32(0);
                    AdminRecord.AdminUserName = reader.GetString(1);
                    AdminRecord.AdminEmail = reader.GetString(2);
                    AdminRecord.AdminPassword = reader.GetString(3);
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
                command.CommandText = "DELETE Admin WHERE AdminId = @AdminID";
                command.Parameters.AddWithValue("@AdminID", AdminRecord.AdminID);
                command.ExecuteNonQuery();
            }

            conn.Close();
            return RedirectToPage("/Admins/AdminView");
        }
    }
}
