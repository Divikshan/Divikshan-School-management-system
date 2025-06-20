using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Repositories
{
    public class MarksRepository
    {
        private string connectionString = "Data Source=DBconnection.db";
        private SQLiteConnection _connection;

        // ✅ Default constructor (existing logic untouched)
        public MarksRepository() { }

        // ✅ New constructor for injected connection
        public MarksRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        // Get all marks (existing)
        public List<Marks> GetAllMarks()
        {
            var list = new List<Marks>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Marks";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Marks
                        {
                            MarkID = reader.GetInt32(0),
                            StudentID = reader.GetInt32(1),
                            SubjectID = reader.GetInt32(2),
                            ExamID = reader.GetInt32(3),
                            MarksObtained = reader.GetInt32(4)
                        });
                    }
                }
            }
            return list;
        }

        // ✅ Get marks by student ID (uses _connection if available)
        public List<Marks> GetMarksByStudentId(int studentId)
        {
            var list = new List<Marks>();
            using (var conn = _connection ?? new SQLiteConnection(connectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM Marks WHERE StudentID = @StudentID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Marks
                            {
                                MarkID = reader.GetInt32(0),
                                StudentID = reader.GetInt32(1),
                                SubjectID = reader.GetInt32(2),
                                ExamID = reader.GetInt32(3),
                                MarksObtained = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            return list;
        }

        // Add mark (existing)
        public bool AddMark(Marks mark)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Marks (StudentID, SubjectID, ExamID, MarksObtained) VALUES (@StudentID, @SubjectID, @ExamID, @MarksObtained)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", mark.StudentID);
                    cmd.Parameters.AddWithValue("@SubjectID", mark.SubjectID);
                    cmd.Parameters.AddWithValue("@ExamID", mark.ExamID);
                    cmd.Parameters.AddWithValue("@MarksObtained", mark.MarksObtained);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        // Update mark (existing)
        public bool UpdateMark(Marks mark)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Marks SET StudentID=@StudentID, SubjectID=@SubjectID, ExamID=@ExamID, MarksObtained=@MarksObtained WHERE MarkID=@MarkID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", mark.StudentID);
                    cmd.Parameters.AddWithValue("@SubjectID", mark.SubjectID);
                    cmd.Parameters.AddWithValue("@ExamID", mark.ExamID);
                    cmd.Parameters.AddWithValue("@MarksObtained", mark.MarksObtained);
                    cmd.Parameters.AddWithValue("@MarkID", mark.MarkID);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        // Delete mark (existing)
        public bool DeleteMark(int markID)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Marks WHERE MarkID=@MarkID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MarkID", markID);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        // ✅ NEW: Get marks by Lecturer ID
        public List<Marks> GetMarksByLecturerId(int lecturerId)
        {
            var list = new List<Marks>();
            using (var conn = _connection ?? new SQLiteConnection(connectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "SELECT * FROM Marks WHERE LecturerID = @LecturerID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LecturerID", lecturerId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Marks
                            {
                                MarkID = reader.GetInt32(0),
                                StudentID = reader.GetInt32(1),
                                SubjectID = reader.GetInt32(2),
                                ExamID = reader.GetInt32(3),
                                MarksObtained = reader.GetInt32(4)
                                // You can also add LecturerID if needed in the model
                            });
                        }
                    }
                }
            }
            return list;
        }

        // ✅ NEW: Update only the MarksObtained by MarkID
        public void UpdateMark(int markId, int newMark)
        {
            using (var conn = _connection ?? new SQLiteConnection(connectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string query = "UPDATE Marks SET MarksObtained = @MarksObtained WHERE MarkID = @MarkID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MarksObtained", newMark);
                    cmd.Parameters.AddWithValue("@MarkID", markId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
