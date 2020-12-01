using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Models;
using Group_Project.Pages.DatabaseConnection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.UploadFile
{
    public class UploadFileModel : PageModel
    {
        
        [BindProperty(SupportsGet = true)]
        public IFormFile MembFile { get; set; }
        [BindProperty(SupportsGet = true)]
        public Member MemberRec { get; set; }

        

        public readonly IWebHostEnvironment _env;


        //a constructor for the class
        public UploadFileModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult OnPost()
        {
            var FileToUpload = Path.Combine(_env.WebRootPath, "Files", MembFile.FileName);//this variable consists of file path
            Console.WriteLine("File Name : " + FileToUpload);
            //System.IO.DirectoryNotFoundException: 'Could not find a part of the path 'C:\Users\elias\source\Latest\wwwroot\Files\Coding.jpg'.'
            using (var FStream = new FileStream(FileToUpload, FileMode.Create))
            {
                MembFile.CopyTo(FStream);//copy the file into FStream variable
            }
            Database_Connection dbstring = new Database_Connection(); 
            string DbConnection = dbstring.DatabaseString(); 
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();
            // only inserts a new member with a file, could be used in create new member
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT Member (MemberName, FileName) VALUES (@MembName, @FName)";
                command.Parameters.AddWithValue("@MembName", MemberRec.MemberName);
                command.Parameters.AddWithValue("@FName", MembFile.FileName);
                Console.WriteLine("File name : " + MemberRec.MemberName);
                Console.WriteLine("File name : " + MembFile.FileName);
                command.ExecuteNonQuery();
            }
            return RedirectToPage("/index");

        }
    }
}