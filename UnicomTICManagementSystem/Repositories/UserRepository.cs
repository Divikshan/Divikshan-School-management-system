using System;
using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Utilities;

namespace UnicomTICManagementSystem.Repositories
{
    public class UserRepository
    {
        public List<Users> GetAllUsers()
        {
            var users = new List<Users>();
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open(); // **Open connection!**

                string query = "SELECT UserID, Username, Password, Role FROM Users";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new Users
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString()
                        });
                    }
                }

                conn.Close(); // Close connection
            }
            return users;
        }

        public bool AddUser(Users user)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();

                // Enable foreign keys in SQLite (good practice)
                using (var pragmaCmd = new SQLiteCommand("PRAGMA foreign_keys = ON;", conn))
                {
                    pragmaCmd.ExecuteNonQuery();
                }

                // Check if username exists to avoid duplicate
                string checkQuery = "SELECT COUNT(1) FROM Users WHERE Username = @username";
                using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@username", user.Username);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        conn.Close();
                        return false; // Username exists
                    }
                }

                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                return true;
            }
        }

        public bool UpdateUser(Users user)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();

                // Check if new username exists for other users
                string checkQuery = "SELECT COUNT(1) FROM Users WHERE Username = @username AND UserID != @userID";
                using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@username", user.Username);
                    checkCmd.Parameters.AddWithValue("@userID", user.UserID);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        conn.Close();
                        return false; // Username conflict
                    }
                }

                string query = "UPDATE Users SET Username=@username, Password=@password, Role=@role WHERE UserID=@userID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.Parameters.AddWithValue("@userID", user.UserID);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                return true;
            }
        }

        public void DeleteUser(int userID)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Users WHERE UserID=@userID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public bool IsUsernameExists(string username, int excludeUserId = 0)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();

                string query = "SELECT COUNT(1) FROM Users WHERE Username = @username AND UserID != @excludeUserId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@excludeUserId", excludeUserId);
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    return count > 0;
                }
            }
        }
    }
}
