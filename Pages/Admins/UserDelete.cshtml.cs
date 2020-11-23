using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Admins
{
    public class UserDeleteModel : PageModel
    {
        [BindProperty]
        public Models.Users UserRecord { get; set; }

        public IActionResult OnGet(int? id)
        {

            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\disko\OneDrive\1. Sheffiled Hallam University\Databases and Web\web\Assignment\Group\Group_Project_Assignment\Databases\Database.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Userz WHERE UserId = @MemberID";
                command.Parameters.AddWithValue("@MemberID", id);

                SqlDataReader reader = command.ExecuteReader();
                UserRecord = new Models.Users();
                while (reader.Read())
                {
                    UserRecord.MemberID = reader.GetInt32(0);
                    UserRecord.UserName = reader.GetString(1);
                    UserRecord.UserEmail = reader.GetString(2);
                    UserRecord.UserCard = reader.GetString(3);
                    UserRecord.UserPassword = reader.GetString(4);
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
                command.CommandText = "DELETE Userz WHERE UserId = @MemberID";
                command.Parameters.AddWithValue("@MemberID", UserRecord.MemberID);
                command.ExecuteNonQuery();
            }

            conn.Close();
            return RedirectToPage("/Admins/AdminView");
        }


    }
}
