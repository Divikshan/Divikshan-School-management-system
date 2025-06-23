using System;
using System.Linq;
using System.Windows.Forms;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageSubjectsForm : Form
    {
        private SubjectController subjectController = new SubjectController();
        private CourseRepository courseRepo = new CourseRepository();

        public ManageSubjectsForm()
        {
            InitializeComponent();
            this.Load += ManageSubjectsForm_Load; // keep form load event
        }

        private void ManageSubjectsForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadSubjects();
        }

        private void LoadCourses()
        {
            cmbCourse.DataSource = courseRepo.GetAllCourses();
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseID";
        }

        private void LoadSubjects()
        {
            dgvSubject.DataSource = null;
            dgvSubject.DataSource = subjectController.GetAllSubjects();
        }

        private void ClearInputs()
        {
            txtSubject.Clear();
            if (cmbCourse.Items.Count > 0) cmbCourse.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Please enter a subject name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var subject = new Subject
            {
                SubjectName = txtSubject.Text.Trim(),
                CourseID = Convert.ToInt32(cmbCourse.SelectedValue)
            };
            subjectController.AddSubject(subject);
            LoadSubjects();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSubject.SelectedRows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(txtSubject.Text))
                {
                    MessageBox.Show("Please enter a subject name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var subject = new Subject
                {
                    SubjectID = Convert.ToInt32(dgvSubject.SelectedRows[0].Cells["SubjectID"].Value),
                    SubjectName = txtSubject.Text.Trim(),
                    CourseID = Convert.ToInt32(cmbCourse.SelectedValue)
                };
                subjectController.UpdateSubject(subject);
                LoadSubjects();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSubject.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvSubject.SelectedRows[0].Cells["SubjectID"].Value);
                subjectController.DeleteSubject(id);
                LoadSubjects();
                ClearInputs();
            }
        }

        private void dgvSubject_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSubject.SelectedRows.Count > 0)
            {
                txtSubject.Text = dgvSubject.SelectedRows[0].Cells["SubjectName"].Value.ToString();
                cmbCourse.SelectedValue = Convert.ToInt32(dgvSubject.SelectedRows[0].Cells["CourseID"].Value);
            }
        }

        // ✅ Back button logic only — no disturbance
        private readonly Form adminForm;

        public ManageSubjectsForm(Form callingForm) : this()
        {
            adminForm = callingForm;
        }

        private void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
            adminForm.Show();
        }
    }
}
