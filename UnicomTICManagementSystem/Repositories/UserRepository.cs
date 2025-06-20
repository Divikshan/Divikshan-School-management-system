using System;
using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Utilities;

namespace UnicomTICManagementSystem.Repositories
{
    public class UserRepository
    {
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "SELECT UserID, Username, Password, Role FROM Users";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString()
                        });
                    }
                }
            }
            return users;
        }

        public void AddUser(User user)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "UPDATE Users SET Username=@username, Password=@password, Role=@role WHERE UserID=@userID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.Parameters.AddWithValue("@userID", user.UserID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int userID)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                string query = "DELETE FROM Users WHERE UserID=@userID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
