using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class StudentsForm : Form
    {
        private StudentController studentController = new StudentController();
        private string connectionString = "Data Source=DBconnection.db";
        private Form adminDashboard;

        // Constructor with AdminDashboardForm reference
        public StudentsForm(Form dashboard)
        {
            InitializeComponent();
            adminDashboard = dashboard;
            LoadCourses();
            LoadStudents();
        }

        private void StudentsForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadStudents();
        }

        private void LoadCourses()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Courses";  // Ensure table is correct
                using (var da = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbCourse.DataSource = dt;
                    cmbCourse.DisplayMember = "CourseName";
                    cmbCourse.ValueMember = "CourseID";
                }
            }
        }

        private void LoadStudents()
        {
            dgvStudents.DataSource = null;
            dgvStudents.DataSource = studentController.GetAllStudents();
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            if (cmbCourse.Items.Count > 0)
                cmbCourse.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var student = new Student
            {
                StudentName = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                CourseId = Convert.ToInt32(cmbCourse.SelectedValue)
            };
            studentController.AddStudent(student);
            LoadStudents();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var student = new Student
                {
                    StudentId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["vStudentId"].Value),
                    StudentName = txtName.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    CourseId = Convert.ToInt32(cmbCourse.SelectedValue)
                };
                studentController.UpdateStudent(student);
                LoadStudents();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["vStudentId"].Value);
                studentController.DeleteStudent(id);
                LoadStudents();
                ClearInputs();
            }
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var row = dgvStudents.SelectedRows[0];
                txtName.Text = row.Cells["StudentName"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                cmbCourse.SelectedValue = Convert.ToInt32(row.Cells["CourseId"].Value);
            }
        }

        // ✅ Back button handler
        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminDashboard.Show();
        }
    }
}
