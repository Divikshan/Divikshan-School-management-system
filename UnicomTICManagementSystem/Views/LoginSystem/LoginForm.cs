using System;
using System.Windows.Forms;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Views.Admin;    // AdminDashboardForm
using UnicomTICManagementSystem.Views;          // StudentDashboardForm, LecturerDashboardForm, StaffDashboardForm

namespace UnicomTICManagementSystem.Views
{
    public partial class LoginForm : Form
    {
        private string connectionString = "Data Source=DBconnection.db";

        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin; // Press Enter to login
            btnLogin.Click += btnLogin_Click;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Admin hardcoded login
            if (username == "admin" && password == "Admin@123")
            {
                MessageBox.Show("Admin login successful!");
                var adminDashboard = new AdminDashboardForm();
                adminDashboard.Show();
                this.Hide();
                return;
            }

            // Try student login
            Student student = AuthenticateStudent(username, password);
            if (student != null)
            {
                var connection = new SQLiteConnection(connectionString);
                connection.Open();

                this.Hide();
                var studentDashboard = new StudentDashboardForm(student, connection);
                studentDashboard.Show();
                return;
            }

            // Try lecturer login
            Lecturer lecturer = AuthenticateLecturer(username, password);
            if (lecturer != null)
            {
                var connection = new SQLiteConnection(connectionString);
                connection.Open();

                this.Hide();
                var lecturerDashboard = new LecturerDashboardForm(lecturer, connection);
                lecturerDashboard.Show();
                return;
            }

            // Try staff login
            Staff staff = AuthenticateStaff(username, password);
            if (staff != null)
            {
                var connection = new SQLiteConnection(connectionString);
                connection.Open();

                this.Hide();
                var staffDashboard = new StaffDashboardForm(staff, connection);
                staffDashboard.Show();
                return;
            }

            // If none match
            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private Student AuthenticateStudent(string username, string password)
        {
            Student student = null;

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Students WHERE Username = @username AND Password = @password";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                StudentId = Convert.ToInt32(reader["StudentID"]),
                                StudentName = reader["Name"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                CourseId = Convert.ToInt32(reader["CourseID"])
                            };
                        }
                    }
                }
            }

            return student;
        }

        private Lecturer AuthenticateLecturer(string username, string password)
        {
            Lecturer lecturer = null;

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Lecturers WHERE Username = @username AND Password = @password";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lecturer = new Lecturer
                            {
                                LecturerID = Convert.ToInt32(reader["LecturerID"]),
                                Name = reader["Name"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Role = "Lecturer"
                            };
                        }
                    }
                }
            }

            return lecturer;
        }

        private Staff AuthenticateStaff(string username, string password)
        {
            Staff staff = null;

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Staff WHERE Username = @username AND Password = @password";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            staff = new Staff
                            {
                                StaffID = Convert.ToInt32(reader["StaffID"]),
                                Name = reader["Name"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Role = "Staff"
                            };
                        }
                    }
                }
            }

            return staff;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Optional: Any code you want on form load
        }
    }
}
