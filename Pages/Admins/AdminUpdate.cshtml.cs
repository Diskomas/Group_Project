using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Admins
{
    public class AdminUpdateModel : PageModel
    {
        [BindProperty]
        public Models.Admins AdminRecords { get; set; }

        public IActionResult OnGet(int? id)
        {
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\disko\OneDrive\1. Sheffiled Hallam University\Databases and Web\web\Assignment\Group\Group_Project_Assignment\Databases\Database.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();


            AdminRecords = new Models.Admins();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Admin WHERE AdminId = @AdminID";

                command.Parameters.AddWithValue("@AdminID", id);
                Console.WriteLine("The id : " + id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AdminRecords.AdminID = reader.GetInt32(0);
                    AdminRecords.AdminUserName = reader.GetString(1);
                    AdminRecords.AdminEmail = reader.GetString(2);
                    AdminRecords.AdminPassword = reader.GetString(3);
                }

            }

            conn.Close();

            return Page();

        }

        public IActionResult OnPost()
        {
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\disko\OneDrive\1. Sheffiled Hallam University\Databases and Web\web\Assignment\Group\Group_Project_Assignment\Databases\Database.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Admin SET AdminUsername = @AdminUserName, AdminEmail = @AdminEmail, AdminPasword = @AdminPassword WHERE AdminId = @AdminID";

                command.Parameters.AddWithValue("@AdminID", AdminRecords.AdminID);
                command.Parameters.AddWithValue("@AdminUserName", AdminRecords.AdminUserName);
                command.Parameters.AddWithValue("@AdminEmail", AdminRecords.AdminEmail);
                command.Parameters.AddWithValue("@AdminPassword", AdminRecords.AdminPassword);

                command.ExecuteNonQuery();
            }

            conn.Close();

            return RedirectToPage("/Admins/AdminView");
        }
    }
}
