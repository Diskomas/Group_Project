using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Admins.AdminPages
{
    public class AdminDeleteModel : PageModel
    {
        [BindProperty]
        public Models.Admins AdminRecord { get; set; }

        public string AdminUserName;
        public const string SessionKeyName4 = "adminusername";


        public string AdminEmail;
        public const string SessionKeyName5 = "adminemail";

        public string SessionID;
        public const string SessionKeyName6 = "sessionID";
        public IActionResult OnGet(int? id)
        {
            // getting the Admin session
            AdminUserName = HttpContext.Session.GetString(SessionKeyName4);
            AdminEmail = HttpContext.Session.GetString(SessionKeyName5);
            SessionID = HttpContext.Session.GetString(SessionKeyName6);

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
            return RedirectToPage("/Admins/AdminPages/AdminView");
        }
    }
}