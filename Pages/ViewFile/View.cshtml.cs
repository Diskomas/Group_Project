using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Models;
using Group_Project.Pages.DatabaseConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.ViewFile
{
    public class ViewModel : PageModel
    {
        public List<Member> FileRec { get; set; }
        public void OnGet()
        {
            Database_Connection dbstring = new Database_Connection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Member";

                var reader = command.ExecuteReader();

                FileRec = new List<Member>();

                while (reader.Read())
                {
                    Member rec = new Member();
                    rec.Memberid = reader.GetInt32(0); // we need this to send the Id to Delete page for another enquiry
                    rec.MemberName = reader.GetString(1);
                    rec.FileName = reader.GetString(2);
                    FileRec.Add(rec);
                }
            }

        }
    }
}