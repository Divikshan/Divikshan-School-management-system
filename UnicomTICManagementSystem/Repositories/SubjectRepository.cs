using System;
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
                // FIXED: Changed "Subject" to "Subjects" (table name consistency)
                var query = "INSERT INTO Subjects (SubjectName, CourseID) VALUES (@name, @courseId)";
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
                // FIXED: Changed "Subject" to "Subjects"
                var query = "UPDATE Subjects SET SubjectName = @name, CourseID = @courseId WHERE SubjectID = @id";
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
                // FIXED: Changed "Subject" to "Subjects"
                var query = "DELETE FROM Subjects WHERE SubjectID = @id";
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
                string query = "SELECT * FROM Subjects";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjects.Add(new Subject
                        {
                            SubjectID = Convert.ToInt32(reader["SubjectID"]),
                            SubjectName = reader["SubjectName"].ToString(),
                            CourseID = Convert.ToInt32(reader["CourseID"])
                        });
                    }
                }
            }
            return subjects;
        }
    }
}
