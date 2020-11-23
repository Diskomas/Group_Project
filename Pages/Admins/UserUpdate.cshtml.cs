using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Admins
{
    public class UserUpdateModel : PageModel
    {
        [BindProperty]
        public Models.Users UserRecords { get; set; }

        public IActionResult OnGet(int? id)
        {
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\disko\OneDrive\1. Sheffiled Hallam University\Databases and Web\web\Assignment\Group\Group_Project_Assignment\Databases\Database.mdf;Integrated Security=True";

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
                command.CommandText = "UPDATE Userz SET UserName = @UserName, UserEmail = @UserEmail, UserCardNumber = @UserCard, UserPassword = @UserPassword WHERE UserId = @MemberID";

                command.Parameters.AddWithValue("@MemberID", UserRecords.MemberID);
                command.Parameters.AddWithValue("@UserName", UserRecords.UserName);
                command.Parameters.AddWithValue("@UserEmail", UserRecords.UserEmail);
                command.Parameters.AddWithValue("@UserCard", UserRecords.UserCard);
                command.Parameters.AddWithValue("@UserPassword", UserRecords.UserPassword);

                command.ExecuteNonQuery();
            }

            conn.Close();

            return RedirectToPage("/Admins/AdminView");
        }


    }
}
