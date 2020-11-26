using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Pages.DatabaseConnection
{
    public class Database_Connection
    {
        public string DatabaseString()
        {                       // UPDATE ME! DATABSE STRING 
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\disko\OneDrive\1. Sheffiled Hallam University\Databases and Web\web\Assignment\Group\Group_Project_Assignment\Databases\Database.mdf;Integrated Security=True";
            return DbString;
        }
    }
}
