using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views.UsersSystem
{
    public partial class ManageUsersForm : Form
    {
        private Form parentForm;
        private UserController userController = new UserController();
        private int selectedUserId = -1;

        public ManageUsersForm(Form parent)
        {
            InitializeComponent();

            parentForm = parent ?? throw new ArgumentNullException(nameof(parent));

            // Only Lecturer, Staff, Student roles allowed here (Admin excluded)
            cmbRole.Items.AddRange(new string[] { "Lecturer", "Staff", "Student" });

            LoadUsers();
        }

        private void ManageUsersForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            List<Users> users = userController.GetAllUsers();

            // Remove admin users from display (safety)
            users.RemoveAll(u => u.Role == "Admin" || u.Username.ToLower() == "admin");

            dgvUsers.DataSource = null;
            dgvUsers.DataSource = users;

            if (dgvUsers.Columns["Password"] != null)
                dgvUsers.Columns["Password"].Visible = false;

            ClearInputs();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var user = new Users
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                Role = cmbRole.SelectedItem.ToString()
            };

            bool added = userController.AddUser(user);

            if (added)
            {
                MessageBox.Show("User added successfully.", "Success");
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Failed to add user. Username may already exist or inputs are invalid.", "Error");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedUserId == -1)
            {
                MessageBox.Show("Select a user to update.", "Warning");
                return;
            }

            if (!ValidateInputs()) return;

            var user = new Users
            {
                UserID = selectedUserId,
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                Role = cmbRole.SelectedItem.ToString()
            };

            bool updated = userController.UpdateUser(user);

            if (updated)
            {
                MessageBox.Show("User updated successfully.", "Success");
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Failed to update user. Username may already exist or inputs are invalid.", "Error");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedUserId == -1)
            {
                MessageBox.Show("Select a user to delete.", "Warning");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                bool deleted = userController.DeleteUser(selectedUserId);

                if (deleted)
                {
                    MessageBox.Show("User deleted successfully.", "Success");
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Failed to delete user.", "Error");
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            parentForm.Show();
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.DataSource == null || dgvUsers.CurrentRow == null) return;

            var user = dgvUsers.CurrentRow.DataBoundItem as Users;
            if (user != null)
            {
                selectedUserId = user.UserID;
                txtUsername.Text = user.Username;
                txtPassword.Text = user.Password;
                cmbRole.SelectedItem = user.Role;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error");
                txtUsername.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Validation Error");
                txtPassword.Focus();
                return false;
            }
            if (cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a role.", "Validation Error");
                cmbRole.DroppedDown = true;
                return false;
            }
            return true;
        }

        private void ClearInputs()
        {
            selectedUserId = -1;
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
            dgvUsers.ClearSelection();
        }
    }
}
