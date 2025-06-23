using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Utilities;

namespace UnicomTICManagementSystem.Repositories
{
    public class StaffRepository
    {
        private SQLiteConnection connection;

        public StaffRepository()
        {
            connection = DatabaseManager.GetConnection();
        }

        public void AddStaff(Staff staff)
        {
            string query = "INSERT INTO Staff (Name, Username, Password) VALUES (@Name, @Username, @Password)";
            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", staff.Name);
                cmd.Parameters.AddWithValue("@Username", staff.Username);
                cmd.Parameters.AddWithValue("@Password", staff.Password);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStaff(Staff staff)
        {
            string query = "UPDATE Staff SET Name = @Name, Username = @Username, Password = @Password WHERE StaffID = @StaffID";
            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", staff.Name);
                cmd.Parameters.AddWithValue("@Username", staff.Username);
                cmd.Parameters.AddWithValue("@Password", staff.Password);
                cmd.Parameters.AddWithValue("@StaffID", staff.StaffID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStaff(int staffId)
        {
            string query = "DELETE FROM Staff WHERE StaffID = @StaffID";
            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@StaffID", staffId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Staff> GetAllStaff()
        {
            List<Staff> staffList = new List<Staff>();
            string query = "SELECT * FROM Staff";
            using (var cmd = new SQLiteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    staffList.Add(new Staff
                    {
                        StaffID = int.Parse(reader["StaffID"].ToString()),
                        Name = reader["Name"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString()
                    });
                }
            }
            return staffList;
        }
    }
}
