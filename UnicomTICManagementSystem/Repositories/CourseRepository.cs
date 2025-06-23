using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Utilities;

namespace UnicomTICManagementSystem.Repositories
{
    public class CourseRepository
    {
        private readonly string connectionString;

        public CourseRepository()
        {
            // Use the connection string from DatabaseManager
            connectionString = DatabaseManager.GetConnectionString();

            // Ensure database is initialized
            DatabaseManager.InitializeDatabase();
        }

        public void AddCourse(Course course)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Courses (CourseName) VALUES (@CourseName)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Database error adding course: {ex.Message}");
                throw;
            }
        }

        public void UpdateCourse(Course course)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Courses SET CourseName = @CourseName WHERE CourseID = @CourseID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                        cmd.Parameters.AddWithValue("@CourseID", course.CourseID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Database error updating course: {ex.Message}");
                throw;
            }
        }

        public void DeleteCourse(int courseId)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Courses WHERE CourseID = @CourseID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", courseId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Database error deleting course: {ex.Message}");
                throw;
            }
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();

            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Courses";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                CourseID = Convert.ToInt32(reader["CourseID"]),
                                CourseName = reader["CourseName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Database error retrieving courses: {ex.Message}");
                MessageBox.Show("Error accessing the database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return courses;
        }

        public Course GetCourseById(int courseId)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Courses WHERE CourseID = @CourseID";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", courseId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Course
                                {
                                    CourseID = Convert.ToInt32(reader["CourseID"]),
                                    CourseName = reader["CourseName"].ToString()
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Database error getting course by ID: {ex.Message}");
                throw;
            }
        }
    }

}