using System;
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

        public void AddStudent(Students student)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Students (StudentName, Username, Password, CourseID) 
                               VALUES (@StudentName, @Username, @Password, @CourseID)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                    cmd.Parameters.AddWithValue("@Username", student.Username);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(student.Password));
                    cmd.Parameters.AddWithValue("@CourseID", student.CourseID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(Students student)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Students 
                               SET StudentName=@StudentName, Username=@Username, 
                                   Password=@Password, CourseID=@CourseID 
                               WHERE StudentID=@StudentID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
                    cmd.Parameters.AddWithValue("@Username", student.Username);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(student.Password));
                    cmd.Parameters.AddWithValue("@CourseID", student.CourseID);
                    cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Students WHERE StudentID=@StudentID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Students> GetAllStudents()
        {
            var students = new List<Students>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT StudentID, StudentName, Username, CourseID FROM Students";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Students
                        {
                            StudentID = reader.GetInt32(0),
                            StudentName = reader.GetString(1),
                            Username = reader.GetString(2),
                            CourseID = reader.GetInt32(3)
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
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}