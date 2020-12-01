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
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\elias\source\Latest\Databases\Database.mdf;Integrated Security=True;Connect Timeout=30";
            return DbString;
        }
    }
}
