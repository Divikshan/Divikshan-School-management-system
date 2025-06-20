using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Repositories
{
    public class SubjectRepository
    {
        private string connectionString = "Data Source=DBconnection.db";

        public void AddSubject(Subject subject)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var query = "INSERT INTO Subject (SubjectName, CourseID) VALUES (@name, @courseId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", subject.SubjectName);
                    cmd.Parameters.AddWithValue("@courseId", subject.CourseID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateSubject(Subject subject)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var query = "UPDATE Subject SET SubjectName = @name, CourseID = @courseId WHERE SubjectID = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", subject.SubjectName);
                    cmd.Parameters.AddWithValue("@courseId", subject.CourseID);
                    cmd.Parameters.AddWithValue("@id", subject.SubjectID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSubject(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var query = "DELETE FROM Subject WHERE SubjectID = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Subject> GetAllSubjects()
        {
            var subjects = new List<Subject>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM Subject";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjects.Add(new Subject
                        {
                            SubjectID = int.Parse(reader["SubjectID"].ToString()),
                            SubjectName = reader["SubjectName"].ToString(),
                            CourseID = int.Parse(reader["CourseID"].ToString())
                        });
                    }
                }
            }
            return subjects;
        }
    }
}
