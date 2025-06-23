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


        }



        private void ManageCoursesForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void LoadCourses()
        {
            dgvCourses.DataSource = null;
            dgvCourses.DataSource = courseController.GetAllCourses();
            dgvCourses.ClearSelection();
        }

        private void ClearInputs()
        {
            txtCourses.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourses.Text))
            {
                MessageBox.Show("Course name cannot be empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var course = new Course { CourseName = txtCourses.Text.Trim() };
            courseController.AddCourse(course);
            LoadCourses();
            ClearInputs();
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a course to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvCourses.SelectedRows[0].DataBoundItem is Course selectedCourse)
            {
                selectedCourse.CourseName = txtCourses.Text.Trim();
                courseController.UpdateCourse(selectedCourse);
                LoadCourses();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a course to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvCourses.SelectedRows[0].DataBoundItem is Course selectedCourse)
            {
                courseController.DeleteCourse(selectedCourse.CourseID);
                LoadCourses();
                ClearInputs();
            }
        }

        private void dgvCourses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0 && dgvCourses.SelectedRows[0].DataBoundItem is Course selectedCourse)
            {
                txtCourses.Text = selectedCourse.CourseName;
            }
        }

        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
            adminForm.Show();
        }
    }
}
