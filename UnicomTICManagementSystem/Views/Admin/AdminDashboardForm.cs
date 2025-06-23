using System;
using System.Data.SQLite;
using System.Windows.Forms;



namespace UnicomTICManagementSystem.Views.Admin
{
   public partial class AdminDashboardForm : Form
    {


        public AdminDashboardForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += new System.EventHandler(this.AdminDashboardForm_Load);

        }

        private void BCourses_Click(object sender, EventArgs e)
        {

            this.Hide();
            ManageSubjectsForm form = new ManageSubjectsForm(this);
            form.ShowDialog();

        }

        private void BSubjects_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageSubjectsForm form = new ManageSubjectsForm(this);
            form.ShowDialog();
        }

        private void BStudents_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentsForm form = new StudentsForm(this);
            form.ShowDialog();
        }

        private void BExams_Click(object sender, EventArgs e)
        {
            this.Hide();
            ExamForm form = new ExamForm(this);
            form.ShowDialog();
        }

        private void BMarks_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ManageMarksForm form = new ManageMarksForm(this);
            form.ShowDialog();
        }

        private void BRoom_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            RoomForm form = new RoomForm(this);
            form.ShowDialog();
        }


        private void BTimetable_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageTimetableForm form = new ManageTimetableForm(this);
            form.ShowDialog();
        }

        private void BStaff_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageStaffForm form = new ManageStaffForm(this);
            form.ShowDialog();
        }

        private void BLecturers_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageLecturersForm form = new ManageLecturersForm(this);
            form.ShowDialog();
        }

        private void BLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Direct connection string - replace with your actual path
                string connectionString = "Data Source=DBconnection.db;Version=3;";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Database connected successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}");
            }
        }
    }
}
