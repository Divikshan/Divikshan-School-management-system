using System;
using System.Windows.Forms;


namespace UnicomTICManagementSystem.Views.Admin
{
   public partial class AdminDashboardForm : Form
    {


        public AdminDashboardForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void BCourses_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageCoursesForm form = new ManageCoursesForm(this);
            form.Show();
        }

        private void BSubjects_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageSubjectsForm form = new ManageSubjectsForm(this);
            form.Show();
        }

        private void BStudents_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentsForm form = new StudentsForm(this);
            form.Show();
        }

        private void BExams_Click(object sender, EventArgs e)
        {
            this.Hide();
            ExamForm form = new ExamForm(this);
            form.Show();
        }

        private void BMarks_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ManageMarksForm form = new ManageMarksForm(this);
            form.Show();
        }


        private void BTimetable_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageTimetableForm form = new ManageTimetableForm(this);
            form.Show();
        }

        private void BStaff_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageStaffForm form = new ManageStaffForm(this);
            form.Show();
        }

        private void BLecturers_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageLecturersForm form = new ManageLecturersForm(this);
            form.Show();
        }

        private void BLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}
