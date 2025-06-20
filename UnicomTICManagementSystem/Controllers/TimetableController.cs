using System;
using System.Collections.Generic;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Utilities;

namespace UnicomTICManagementSystem.Controllers
{
    public class TimetableController
    {
        // Add a new timetable entry
        public void AddTimetable(Timetable timetable)
        {
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();

                string query = @"
                    INSERT INTO Timetables 
                    (CourseID, SubjectID, LecturerID, Date, TimeSlot, Location)
                    VALUES (@CourseID, @SubjectID, @LecturerID, @Date, @TimeSlot, @Location)";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CourseID", timetable.CourseID);
                    cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                    cmd.Parameters.AddWithValue("@LecturerID", timetable.LecturerID);
                    cmd.Parameters.AddWithValue("@Date", timetable.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                    cmd.Parameters.AddWithValue("@Location", timetable.Location);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update existing timetable entry by TimetableID
        public void UpdateTimetable(Timetable timetable)
        {
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();

                string query = @"
                    UPDATE Timetables SET
                        CourseID = @CourseID,
                        SubjectID = @SubjectID,
                        LecturerID = @LecturerID,
                        Date = @Date,
                        TimeSlot = @TimeSlot,
                        Location = @Location
                    WHERE TimetableID = @TimetableID";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CourseID", timetable.CourseID);
                    cmd.Parameters.AddWithValue("@SubjectID", timetable.SubjectID);
                    cmd.Parameters.AddWithValue("@LecturerID", timetable.LecturerID);
                    cmd.Parameters.AddWithValue("@Date", timetable.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@TimeSlot", timetable.TimeSlot);
                    cmd.Parameters.AddWithValue("@Location", timetable.Location);
                    cmd.Parameters.AddWithValue("@TimetableID", timetable.TimetableID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete timetable entry by ID
        public void DeleteTimetable(int timetableId)
        {
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM Timetables WHERE TimetableID = @TimetableID";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TimetableID", timetableId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Get all timetable entries
        public List<Timetable> GetAllTimetables()
        {
            var list = new List<Timetable>();

            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Timetables";

                using (var cmd = new SQLiteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var timetable = new Timetable
                        {
                            TimetableID = Convert.ToInt32(reader["TimetableID"]),
                            CourseID = Convert.ToInt32(reader["CourseID"]),
                            SubjectID = Convert.ToInt32(reader["SubjectID"]),
                            LecturerID = Convert.ToInt32(reader["LecturerID"]),
                            Date = DateTime.Parse(reader["Date"].ToString()),
                            TimeSlot = reader["TimeSlot"].ToString(),
                            Location = reader["Location"].ToString()
                        };
                        list.Add(timetable);
                    }
                }
            }

            return list;
        }

        // Optional: Get timetable by ID
        public Timetable GetTimetableById(int timetableId)
        {
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Timetables WHERE TimetableID = @TimetableID";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TimetableID", timetableId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Timetable
                            {
                                TimetableID = Convert.ToInt32(reader["TimetableID"]),
                                CourseID = Convert.ToInt32(reader["CourseID"]),
                                SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                LecturerID = Convert.ToInt32(reader["LecturerID"]),
                                Date = DateTime.Parse(reader["Date"].ToString()),
                                TimeSlot = reader["TimeSlot"].ToString(),
                                Location = reader["Location"].ToString()
                            };
                        }
                    }
                }
            }

            return null; // Not found
        }
    }
}
