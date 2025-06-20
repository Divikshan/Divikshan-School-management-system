using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Utilities;

namespace UnicomTICManagementSystem.Repositories
{
    public class ExamRepository
    {
        private SQLiteConnection connection;

        public ExamRepository()
        {
            connection = DatabaseManager.GetConnection();
        }

        public void AddExam(Exam exam)
        {
            string query = "INSERT INTO Exams (ExamName, SubjectID) VALUES (@ExamName, @SubjectID)";
            using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                cmd.Parameters.AddWithValue("@SubjectID", exam.SubjectID);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateExam(Exam exam)
        {
            string query = "UPDATE Exams SET ExamName = @ExamName, SubjectID = @SubjectID WHERE ExamID = @ExamID";
            using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ExamName", exam.ExamName);
                cmd.Parameters.AddWithValue("@SubjectID", exam.SubjectID);
                cmd.Parameters.AddWithValue("@ExamID", exam.ExamID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteExam(int examId)
        {
            string query = "DELETE FROM Exams WHERE ExamID = @ExamID";
            using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ExamID", examId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Exam> GetAllExams()
        {
            List<Exam> exams = new List<Exam>();
            string query = "SELECT * FROM Exams";

            using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Exam exam = new Exam
                        {
                            ExamID = Convert.ToInt32(reader["ExamID"]),
                            ExamName = reader["ExamName"].ToString(),
                            SubjectID = Convert.ToInt32(reader["SubjectID"])
                        };
                        exams.Add(exam);
                    }
                }
            }

            return exams;
        }

        public DataTable GetAllExamsWithSubjectNames()
        {
            string query = @"SELECT Exams.ExamID, Exams.ExamName, Subjects.SubjectName 
                             FROM Exams 
                             INNER JOIN Subjects ON Exams.SubjectID = Subjects.SubjectID";

            using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
