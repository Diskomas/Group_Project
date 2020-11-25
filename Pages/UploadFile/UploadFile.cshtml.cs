using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.UploadFile
{
    public class UploadFileModel : PageModel
    {
        [BindProperty]
        public IFormFile StdFile { get; set; }

        public readonly IWebHostEnvironment _env;


        //a constructor for the class
        public UploadFileModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            var FileToUpload = Path.Combine(_env.WebRootPath, "Files", StdFile.FileName);//this variable consists of file path
            Console.WriteLine("File Name : " + FileToUpload);

            using (var FStream = new FileStream(FileToUpload, FileMode.Create))
            {
                StdFile.CopyTo(FStream);//copy the file into FStream variable
            }

            return RedirectToPage("/index");

        }
    }
}