using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Login
{
    public class AdminPassResetModel : PageModel
    {

        [BindProperty]
        public Models.Admins Admin { get; set; }

        public string Message { get; set; }

        public string SessionID;

        public IActionResult OnPost()
        {
            
            DatabaseConnection.Database_Connection dbstring = new DatabaseConnection.Database_Connection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(Admin.AdminUserName);
            Console.WriteLine(Admin.AdminEmail);


            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT AdminUsername, AdminEmail, AdminResetUniq FROM Admin WHERE AdminEmail = @AEmail AND AdminResetUniq = @AUnique";


                // ---------------- VALIDATE IF FIELD CONTAINS DATA ----------------
                if (string.IsNullOrEmpty(Admin.AdminPassword) || string.IsNullOrEmpty(Admin.AdminEmail))
                {
                    command.Parameters.AddWithValue("@AEmail", "randomvalue123");
                    command.Parameters.AddWithValue("@AUnique", "randomvalue123");
                }
                else
                {
                    command.Parameters.AddWithValue("@AEmail", Admin.AdminEmail);
                    command.Parameters.AddWithValue("@AUnique", Admin.AdminReset);
                }
                // ---------------- VALIDATE IF FIELD CONTAINS DATA ----------------

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Admin.AdminUserName = reader.GetString(0);
                }

                reader.Close();

                if (!string.IsNullOrEmpty(Admin.AdminUserName))
                {
                    command.Parameters.AddWithValue("@AdminPassword", Admin.AdminPassword);

                    command.CommandText = "UPDATE Admin SET AdminPasword = @AdminPassword WHERE AdminEmail = @AEmail";

                    command.ExecuteNonQuery();

                    conn.Close();

                    return RedirectToPage("/Login/AdminLogin");

                    
                }
                else
                {
                    if (string.IsNullOrEmpty(Admin.AdminEmail))
                    {
                        Message = "Invalid Email!";
                    }
                    else if (string.IsNullOrEmpty(Admin.AdminReset))
                    {
                        Message = "Invalid Unique variable!";
                    }
                    else
                    {
                        Message = "Invalid Information!";
                    }
                    return Page();
                }
            }
        }
            public void OnGet()
        {
        }
    }
}
