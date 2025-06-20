using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Utilities;


namespace UnicomTICManagementSystem.Repositories
{
    public class LecturerRepository
    {
        private SQLiteConnection connection;

        public LecturerRepository()
        {
            connection = DatabaseManager.GetConnection();
        }

        public void AddLecturer(Lecturer lecturer)
        {
            string query = "INSERT INTO Lecturers (Name, Username, Password) VALUES (@Name, @Username, @Password)";
            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", lecturer.Name);
                cmd.Parameters.AddWithValue("@Username", lecturer.Username);
                cmd.Parameters.AddWithValue("@Password", lecturer.Password);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            string query = "UPDATE Lecturers SET Name = @Name, Username = @Username, Password = @Password WHERE LecturerID = @LecturerID";
            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Name", lecturer.Name);
                cmd.Parameters.AddWithValue("@Username", lecturer.Username);
                cmd.Parameters.AddWithValue("@Password", lecturer.Password);
                cmd.Parameters.AddWithValue("@LecturerID", lecturer.LecturerID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteLecturer(int lecturerId)
        {
            string query = "DELETE FROM Lecturers WHERE LecturerID = @LecturerID";
            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@LecturerID", lecturerId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Lecturer> GetAllLecturers()
        {
            List<Lecturer> lecturers = new List<Lecturer>();
            string query = "SELECT LecturerID, Name, Username, Password FROM Lecturers";

            using (var cmd = new SQLiteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lecturers.Add(new Lecturer
                    {
                        LecturerID = int.Parse(reader["LecturerID"].ToString()),
                        Name = reader["Name"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString()
                    });
                }
            }
            return lecturers;
        }
    }
}
