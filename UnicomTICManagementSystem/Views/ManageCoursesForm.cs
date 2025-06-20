using System;
using System.Windows.Forms;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Controllers;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageCoursesForm : Form
    {
        private readonly CourseController courseController;
        private readonly Form adminForm;

        public ManageCoursesForm(Form callingForm)
        {
            InitializeComponent();
            adminForm = callingForm;
            courseController = new CourseController();

            this.StartPosition = FormStartPosition.CenterScreen;

            // Register form load event
            this.Load += ManageCoursesForm_Load;
        }

        private void ManageCoursesForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void LoadCourses()
        {
            dgvCourses.DataSource = null;
            dgvCourses.DataSource = courseController.GetAllCourses();
        }

        private void ClearInputs()
        {
            txtCourses.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCourses.Text))
            {
                var course = new Course
                {
                    CourseName = txtCourses.Text.Trim()
                };
                courseController.AddCourse(course);
                LoadCourses();
                ClearInputs();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                var course = new Course
                {
                    CourseID = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["CourseID"].Value),
                    CourseName = txtCourses.Text.Trim()
                };
                courseController.UpdateCourse(course);
                LoadCourses();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["CourseID"].Value);
                courseController.DeleteCourse(id);
                LoadCourses();
                ClearInputs();
            }
        }

        private void dgvCourses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                txtCourses.Text = dgvCourses.SelectedRows[0].Cells["CourseName"].Value.ToString();
            }
        }

        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            this.Close();        // Close this form
            adminForm.Show();    // Show the Admin Dashboard again
        }
    }
}
