using System;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageStaffForm : Form
    {
        private readonly StaffController staffController;
        private int selectedStaffId = -1;
        private Form parentForm;

        public ManageStaffForm(Form parent = null)
        {
            InitializeComponent();
            parentForm = parent;
            staffController = new StaffController();
        }

        private void ManageStaffForm_Load(object sender, EventArgs e)
        {
            LoadStaffs();
        }

        private void LoadStaffs()
        {
            dgvStaffs.DataSource = staffController.GetAllStaffs();

            // Hide password column
            if (dgvStaffs.Columns["Password"] != null)
                dgvStaffs.Columns["Password"].Visible = false;

            ClearInputs();
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            selectedStaffId = -1;
            dgvStaffs.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            Staff staff = new Staff
            {
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };

            bool added = staffController.AddStaff(staff);

            if (!added)
            {
                MessageBox.Show("Username already exists. Try another one.", "Duplicate Error");
                return;
            }

            MessageBox.Show("Staff added successfully.");
            LoadStaffs();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedStaffId == -1)
            {
                MessageBox.Show("Please select a staff to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            Staff staff = new Staff
            {
                StaffID = selectedStaffId,
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };

            staffController.UpdateStaff(staff);
            MessageBox.Show("Staff updated successfully.");
            LoadStaffs();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedStaffId == -1)
            {
                MessageBox.Show("Please select a staff to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this staff?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            staffController.DeleteStaff(selectedStaffId);
            MessageBox.Show("Staff deleted successfully.");
            LoadStaffs();
            ClearInputs();
        }

        private void dgvStaffs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStaffs.Rows[e.RowIndex];
                selectedStaffId = Convert.ToInt32(row.Cells["StaffID"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            parentForm?.Show();
        }
    }
}
