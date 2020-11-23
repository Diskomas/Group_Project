using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Group_Project.Pages.Admins
{
    public class AdminViewModel : PageModel
    {
        public List<Models.Admins> AdminRecord { get; set; }
        public List<Models.Users> UserRecord { get; set; }

        public void OnGet()
        {

            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\disko\OneDrive\1. Sheffiled Hallam University\Databases and Web\web\Assignment\Group\Group_Project_Assignment\Databases\Database.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Admin";

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

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Userz";

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

                    UserRecord.Add(record); 
                }

                reader.Close();
            }
        }
    }
}
