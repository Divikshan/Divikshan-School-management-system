using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTICManagementSystem.Utilities
{
    internal class AuthHelper
    {
        private string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DBconnection.db")};Version=3;";

        public string Login(string username, string password)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Role FROM Users WHERE Username = @username AND Password = @password";
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    object result = cmd.ExecuteScalar();

                    return result != null ? result.ToString() : null;
                }
            }
        }
    }
}
    

