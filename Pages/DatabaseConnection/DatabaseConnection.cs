using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Pages.DatabaseConnection
{
    public class DatabaseConnection
    {

        public string DatabaseString()
        {

            string DBString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sara\source\repos\Group_Project\Databases\Database.mdf;Integrated Security=True";
            return DBString;
        }
    }
}