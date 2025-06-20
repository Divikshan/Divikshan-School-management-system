using System;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageStaffForm : Form
    {
        private StaffController staffController;
        private int selectedStaffId = -1;
        private Form parentForm;

        public ManageStaffForm(Form parent = null)
        {
            InitializeComponent();
            parentForm = parent;
        }


        public ManageStaffForm()
        {
            InitializeComponent();
            staffController = new StaffController();
        }

        private void ManageStaffForm_Load(object sender, EventArgs e)
        {
            LoadStaff();
        }

        private void LoadStaff()
        {
            dgvStaffs.DataSource = staffController.GetAllStaffs();
        }

        private void ClearInputs()
        {
            txtName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            selectedStaffId = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff
            {
                Name = txtName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim()
            };

            staffController.AddStaff(staff);
            LoadStaff();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedStaffId == -1)
            {
                MessageBox.Show("Please select a staff to update.");
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
            LoadStaff();
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedStaffId == -1)
            {
                MessageBox.Show("Please select a staff to delete.");
                return;
            }

            staffController.DeleteStaff(selectedStaffId);
            LoadStaff();
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
            if (parentForm != null)
            {
                parentForm.Show();
            }
        }
    }
}
