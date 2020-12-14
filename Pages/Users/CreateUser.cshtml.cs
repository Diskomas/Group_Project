using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Users
{
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public Models.Users Usersdetail { get; set; }

        public string Message { get; set; }
        public IActionResult OnPost()
        {
            DatabaseConnection.Database_Connection dbstring = new DatabaseConnection.Database_Connection();
            string DbConnection = dbstring.DatabaseString();

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO Userz (UserName, UserEmail, UserCardNumber, UserPassword, MembershipId) VALUES (@UserName, @UserEmail, @UserCard, @UserPassword, @MembershipId)";

                command.Parameters.AddWithValue("@UserName", Usersdetail.UserName);
                command.Parameters.AddWithValue("@UserEmail", Usersdetail.UserEmail);
                command.Parameters.AddWithValue("@UserCard", Usersdetail.UserCard);
                command.Parameters.AddWithValue("@UserPassword", Usersdetail.UserPassword);
                command.Parameters.AddWithValue("@MembershipId", Usersdetail.MembershipId);

                if (string.IsNullOrEmpty(Usersdetail.UserName) || string.IsNullOrEmpty(Usersdetail.UserEmail) || string.IsNullOrEmpty(Usersdetail.UserCard) || string.IsNullOrEmpty(Usersdetail.UserPassword))
                {
                    Message = "Please don't leave empty fields!";

                }
                else
                {
                    command.ExecuteNonQuery();
                    return RedirectToPage("/login/Userlogin");
                }   
            }
            return Page();
        }

    }
}
