using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Models
{
    public class MemberFileModel : PageModel
    {
        public int Id { get; set; }

        public string MemberName { get; set; }

        public string FileName { get; set; }

        public void OnGet()
        {
        }
    }
}
