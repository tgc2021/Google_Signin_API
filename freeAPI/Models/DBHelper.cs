using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace freeAPI.Models
{
    public class DBHelper
    {
        private readonly string connectionString;

        public DBHelper()
        {
            // Ensure your connection string is in Web.config or appsettings.json
            connectionString = ConfigurationManager.ConnectionStrings["dbconnectionstring"].ConnectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}