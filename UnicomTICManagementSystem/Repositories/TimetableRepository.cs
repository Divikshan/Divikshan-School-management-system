using System;
using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Repositories
{
    public class TimetableRepository
    {


        // ✅ NEW: Get timetable by lecturer ID
        public List<Timetable> GetTimetableByLecturerId(int lecturerId)
        {
            List<Timetable> timetables = new List<Timetable>();
            string query = @"
        SELECT TimetableID, CourseID, SubjectID, LecturerID, Date, TimeSlot, Location 
        FROM Timetable
        WHERE LecturerID = @LecturerID";

            using (var cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@LecturerID", lecturerId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        timetables.Add(new Timetable
                        {
                            TimetableID = Convert.ToInt32(reader["TimetableID"]),
                            CourseID = Convert.ToInt32(reader["CourseID"]),
                            SubjectID = Convert.ToInt32(reader["SubjectID"]),
                            LecturerID = Convert.ToInt32(reader["LecturerID"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            TimeSlot = reader["TimeSlot"].ToString(),
                            Location = reader["Location"].ToString()
                        });
                    }
                }
            }

            return timetables;
        }

        private SQLiteConnection _connection;

        // ✅ Constructor accepting SQLiteConnection
        public TimetableRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        // Existing method: Get all courses
        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (var conn = Utilities.DatabaseManager.GetConnection())
            {
                conn.Open();
                string query = "SELECT CourseID, CourseName FROM Course";
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
            return courses;
        }

        // Existing method: Get all subjects
        public List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = new List<Subject>();
            using (var conn = Utilities.DatabaseManager.GetConnection())
            {
                conn.Open();
                string query = "SELECT SubjectID, SubjectName FROM Subject";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjects.Add(new Subject
                        {
                            SubjectID = Convert.ToInt32(reader["SubjectID"]),
                            SubjectName = reader["SubjectName"].ToString()
                        });
                    }
                }
            }
            return subjects;
        }

        // Existing method: Get all lecturers
        public List<Lecturer> GetAllLecturers()
        {
            List<Lecturer> lecturers = new List<Lecturer>();
            using (var conn = Utilities.DatabaseManager.GetConnection())
            {
                conn.Open();
                string query = "SELECT LecturerID, LecturerName FROM Lecturer";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lecturers.Add(new Lecturer
                        {
                            LecturerID = Convert.ToInt32(reader["LecturerID"]),
                            Name = reader["LecturerName"].ToString()
                        });
                    }
                }
            }
            return lecturers;
        }

        // Existing method: Get all timetables
        public List<Timetable> GetAllTimetables()
        {
            List<Timetable> timetables = new List<Timetable>();
            using (var conn = Utilities.DatabaseManager.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT TimetableID, CourseID, SubjectID, LecturerID, Date, TimeSlot, Location 
                    FROM Timetable";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        timetables.Add(new Timetable
                        {
                            TimetableID = Convert.ToInt32(reader["TimetableID"]),
                            CourseID = Convert.ToInt32(reader["CourseID"]),
                            SubjectID = Convert.ToInt32(reader["SubjectID"]),
                            LecturerID = Convert.ToInt32(reader["LecturerID"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            TimeSlot = reader["TimeSlot"].ToString(),
                            Location = reader["Location"].ToString()
                        });
                    }
                }
            }
            return timetables;
        }

        // ✅ NEW: Get timetable by course ID
        public List<Timetable> GetTimetableByCourseId(int courseId)
        {
            List<Timetable> timetables = new List<Timetable>();
            string query = @"
                SELECT TimetableID, CourseID, SubjectID, LecturerID, Date, TimeSlot, Location 
                FROM Timetable
                WHERE CourseID = @CourseID";

            using (var cmd = new SQLiteCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        timetables.Add(new Timetable
                        {
                            TimetableID = Convert.ToInt32(reader["TimetableID"]),
                            CourseID = Convert.ToInt32(reader["CourseID"]),
                            SubjectID = Convert.ToInt32(reader["SubjectID"]),
                            LecturerID = Convert.ToInt32(reader["LecturerID"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            TimeSlot = reader["TimeSlot"].ToString(),
                            Location = reader["Location"].ToString()
                        });
                    }
                }
            }
            return timetables;
        }

        // Existing method: Add timetable
        public bool AddTimetable(Timetable timetable)
        {
            using (var conn = Utilities.DatabaseManager.GetConnection())
            {
                conn.Open();
                string query = @"
                    INSERT INTO Timetable (CourseID, SubjectID, LecturerID, Date, TimeSlot, Location)
                    VALUES (@CourseID, @SubjectID, @LecturerID, @Date, @TimeSlot, @Location)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", timetable.CourseID);
                    cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                    cmd.Parameters.AddWithValue("@LecturerID", timetable.LecturerID);
                    cmd.Parameters.AddWithValue("@Date", timetable.Date);
                    cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                    cmd.Parameters.AddWithValue("@Location", timetable.Location);

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        // Existing method: Update timetable
        public bool UpdateTimetable(Timetable timetable)
        {
            using (var conn = Utilities.DatabaseManager.GetConnection())
            {
                conn.Open();
                string query = @"
                    UPDATE Timetable SET
                        CourseID = @CourseID,
                        SubjectID = @SubjectID,
                        LecturerID = @LecturerID,
                        Date = @Date,
                        TimeSlot = @TimeSlot,
                        Location = @Location
                    WHERE TimetableID = @TimetableID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", timetable.CourseID);
                    cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                    cmd.Parameters.AddWithValue("@LecturerID", timetable.LecturerID);
                    cmd.Parameters.AddWithValue("@Date", timetable.Date);
                    cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                    cmd.Parameters.AddWithValue("@Location", timetable.Location);
                    cmd.Parameters.AddWithValue("@TimetableID", timetable.TimetableID);

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        // Existing method: Delete timetable
        public bool DeleteTimetable(int timetableId)
        {
            using (var conn = Utilities.DatabaseManager.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Timetable WHERE TimetableID = @TimetableID";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TimetableID", timetableId);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }
    }
}
