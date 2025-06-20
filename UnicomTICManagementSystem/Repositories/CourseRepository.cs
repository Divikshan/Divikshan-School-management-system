using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Repositories
{
    public class CourseRepository
    {
        private readonly string connectionString = "Data Source=DBconnection.db";

        public void AddCourse(Course course)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Course (CourseName) VALUES (@CourseName)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCourse(Course course)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Course SET CourseName = @CourseName WHERE CourseID = @CourseID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseID", course.CourseID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCourse(int courseId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Course WHERE CourseID = @CourseID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Course";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseID = int.Parse(reader["CourseID"].ToString()),
                            CourseName = reader["CourseName"].ToString()
                        });
                    }
                }
            }
            return courses;
        }
    }
}
