using System.Collections.Generic;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Repositories
{
    public class StudentRepository
    {
        private readonly string connectionString = "Data Source=DBconnection.db";

        public void AddStudent(Student student)
        {
            if (student == null) return;

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Students (StudentName, Username, Password, CourseId) 
                               VALUES (@StudentName, @Username, @Password, @CourseId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                    cmd.Parameters.AddWithValue("@Username", student.Username);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(student.Password)); // Added hashing
                    cmd.Parameters.AddWithValue("@CourseId", student.CourseId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            if (student == null) return;

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Students 
                               SET StudentName=@StudentName, Username=@Username, 
                                   Password=@Password, CourseId=@CourseId 
                               WHERE StudentId=@StudentId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                    cmd.Parameters.AddWithValue("@Username", student.Username);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(student.Password)); // Added hashing
                    cmd.Parameters.AddWithValue("@CourseId", student.CourseId);
                    cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Students WHERE StudentId=@StudentId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT s.StudentId, s.StudentName, s.Username, s.Password, 
                                       s.CourseId, c.CourseName 
                                FROM Students s 
                                JOIN Course c ON s.CourseId = c.CourseId";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentId = reader.GetInt32(0),
                            StudentName = reader.GetString(1),
                            Username = reader.GetString(2),
                            Password = string.Empty, // Don't return actual password
                            CourseId = reader.GetInt32(4)
                        });
                    }
                }
            }
            return students;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Encoding.UTF8.GetString(hashedBytes);
            }
        }
    }
}