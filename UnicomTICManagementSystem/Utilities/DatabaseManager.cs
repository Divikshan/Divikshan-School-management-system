using System;
using System.Data.SQLite;
using System.IO;

namespace UnicomTICManagementSystem.Utilities
{
    internal class DatabaseManager
    {
        public static void AddUser(string username, string password, string role)
        {
            using (var connection = GetConnection())
            {
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@role", role);
                    command.ExecuteNonQuery();
                }
            }
        }


        private const string DatabaseFile = "DBconnection.db";  // Your DB file name
        private const string ConnectionString = "Data Source=" + DatabaseFile + ";Version=3;";

        public static string GetConnectionString()
        {
            return ConnectionString;
        }

        // Returns an OPEN SQLiteConnection
        public static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }


        public static void InitializeDatabase()
        {
            if (!File.Exists(DatabaseFile))
            {
                SQLiteConnection.CreateFile(DatabaseFile);

                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    // Enable foreign key enforcement in SQLite
                    using (var command = new SQLiteCommand("PRAGMA foreign_keys = ON;", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Create tables
                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Users (
                            UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL,
                            Role TEXT NOT NULL CHECK (Role IN ( 'Lecturer', 'Staff', 'Student'))
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Lecturers (
                            LecturerID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            UserID INTEGER UNIQUE,
                            FOREIGN KEY(UserID) REFERENCES Users(UserID) ON DELETE CASCADE
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Staff (
                            StaffID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            UserID INTEGER UNIQUE,
                            FOREIGN KEY(UserID) REFERENCES Users(UserID)
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Courses (
                            CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                            CourseName TEXT NOT NULL
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Subjects (
                            SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                            SubjectName TEXT NOT NULL,
                            CourseID INTEGER NOT NULL,
                            FOREIGN KEY(CourseID) REFERENCES Courses(CourseID) ON DELETE CASCADE
                        )");

                    // In the InitializeDatabase() method, find the Students table creation code
                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Students (
                            StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                            StudentName TEXT NOT NULL,
                            Username TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL, 
                            CourseID INTEGER NOT NULL,
                            FOREIGN KEY(CourseID) REFERENCES Courses(CourseID)
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Rooms (
                            RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                            RoomName TEXT NOT NULL UNIQUE,
                            RoomType TEXT NOT NULL CHECK (RoomType IN ('Lab', 'Hall', 'Classroom'))
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Exams (
                            ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                            ExamName TEXT NOT NULL,
                            SubjectID INTEGER NOT NULL,
                            FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID) ON DELETE CASCADE
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Marks (
                            MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                            StudentID INTEGER NOT NULL,
                            SubjectID INTEGER ,
                            ExamID INTEGER NOT NULL,
                            MarksObtained INTEGER NOT NULL,
                            FOREIGN KEY(StudentID) REFERENCES Students(StudentID)ON DELETE CASCADE,
                            FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)ON DELETE CASCADE,
                            FOREIGN KEY(ExamID) REFERENCES Exams(ExamID)
                        )");


                    // FIXED foreign key here:
                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Timetables (
                            TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                            CourseID INTEGER NOT NULL,
                            SubjectID INTEGER NOT NULL,
                            LecturerID INTEGER NOT NULL,
                            DayOfWeek TEXT NOT NULL CHECK (DayOfWeek IN ('Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday')),
                            StartTime TEXT NOT NULL,  -- Store as 'HH:MM' format
                            EndTime TEXT NOT NULL,
                            RoomID INTEGER NOT NULL,
                            FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
                            FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                            FOREIGN KEY (LecturerID) REFERENCES Lecturers(LecturerID),
                            FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
                            UNIQUE (RoomID, DayOfWeek, StartTime)
                        )");

                 

                    // Insert sample rooms
                    ExecuteNonQuery(connection, @"
                        INSERT INTO Rooms (RoomName, RoomType)
                        VALUES 
                            ('Lab 1', 'Lab'),
                            ('Lab 2', 'Lab'),
                            ('Hall A', 'Hall'),
                            ('Hall B', 'Hall')");
                }
            }
        }

        private static void ExecuteNonQuery(SQLiteConnection connection, string commandText)
        {
            using (var command = new SQLiteCommand(commandText, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
