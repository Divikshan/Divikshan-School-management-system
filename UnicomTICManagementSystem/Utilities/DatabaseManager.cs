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
                        CREATE TABLE Users (
                            UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL,
                            Role TEXT NOT NULL
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE Lecturers (
                            LecturerID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            UserID INTEGER UNIQUE,
                            FOREIGN KEY(UserID) REFERENCES Users(UserID)
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE Staff (
                            StaffID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            UserID INTEGER UNIQUE,
                            FOREIGN KEY(UserID) REFERENCES Users(UserID)
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE Courses (
                            CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                            CourseName TEXT NOT NULL
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE Subjects (
                            SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                            SubjectName TEXT NOT NULL,
                            CourseID INTEGER NOT NULL,
                            FOREIGN KEY(CourseID) REFERENCES Courses(CourseID)
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE Students (
                            vStudentId INTEGER PRIMARY KEY AUTOINCREMENT,
                            StudentName TEXT NOT NULL,
                            Username TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL,
                            CourseId INTEGER NOT NULL,
                            FOREIGN KEY(CourseId) REFERENCES Course(CourseId)
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE Rooms (
                            RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                            RoomName TEXT NOT NULL,
                            RoomType TEXT NOT NULL
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE Exams (
                            ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                            ExamName TEXT NOT NULL,
                            SubjectID INTEGER NOT NULL,
                            FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)
                        )");

                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Marks (
                            MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                            StudentID INTEGER,
                            SubjectID INTEGER,
                            ExamID INTEGER,
                            MarksObtained INTEGER,
                            FOREIGN KEY(StudentID) REFERENCES Students(StudentId),
                            FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID),
                            FOREIGN KEY(ExamID) REFERENCES Exams(ExamID)
                        )");


                    // FIXED foreign key here:
                    ExecuteNonQuery(connection, @"
                        CREATE TABLE Timetables (
                            TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                            CourseID INTEGER NOT NULL,
                            SubjectID INTEGER NOT NULL,
                            LecturerID INTEGER NOT NULL,
                            Date TEXT NOT NULL,
                            TimeSlot TEXT NOT NULL,
                            Location TEXT NOT NULL,
                            FOREIGN KEY(CourseID) REFERENCES Courses(CourseID),
                            FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID),
                            FOREIGN KEY(LecturerID) REFERENCES Lecturers(LecturerID)
                        )");

                    // Insert default admin user (password stored as plain text, consider hashing for production)
                    ExecuteNonQuery(connection, @"
                        INSERT INTO Users (Username, Password, Role)
                        VALUES ('admin', 'admin123', 'Admin')");

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
