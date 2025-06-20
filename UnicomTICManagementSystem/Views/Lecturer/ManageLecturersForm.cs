using System;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageLecturersForm : Form
    {
        private readonly LecturerController lecturerController;
        private int selectedLecturerId = -1;
        private Form parentForm;

        // Constructor with parent reference
        public ManageLecturersForm(Form parent = null)
        {
            InitializeComponent();
            lecturerController = new LecturerController();
            parentForm = parent;
        }

        private void ManageLecturersForm_Load(object sender, EventArgs e)
        {
            LoadLecturers();
        }

        private void LoadLecturers()
        {
            dgvLecturers.DataSource = lecturerController.GetAllLecturers();

            if (dgvLecturers.Columns.Contains("LecturerID"))
                dgvLecturers.Columns["LecturerID"].Visible = false;

            if (dgvLecturers.Columns.Contains("Password"))
                dgvLecturers.Columns["Password"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            var lecturer = new Lecturer
            {
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };

            lecturerController.AddLecturer(lecturer);
            LoadLecturers();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedLecturerId == -1)
            {
                MessageBox.Show("Please select a lecturer to update.");
                return;
            }

            var lecturer = new Lecturer
            {
                LecturerID = selectedLecturerId,
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };

            lecturerController.UpdateLecturer(lecturer);
            LoadLecturers();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedLecturerId == -1)
            {
                MessageBox.Show("Please select a lecturer to delete.");
                return;
            }

            lecturerController.DeleteLecturer(selectedLecturerId);
            LoadLecturers();
            ClearInputs();
        }

        private void dgvLecturers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvLecturers.Rows.Count)
            {
                var row = dgvLecturers.Rows[e.RowIndex];
                selectedLecturerId = Convert.ToInt32(row.Cells["LecturerID"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Clear(); // Always keep password field empty
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            if (parentForm != null)
            {
                parentForm.Show();
            }
        }

        private void ClearInputs()
        {
            selectedLecturerId = -1;
            txtName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            dgvLecturers.ClearSelection();
        }
    }
}
