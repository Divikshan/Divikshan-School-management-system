using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views.UsersSystem
{
    public partial class ManageUsersForm : Form
    {
        private UserController userController = new UserController();
        private int selectedUserId = -1;

        public ManageUsersForm()
        {
            InitializeComponent();
            cmbRole.Items.AddRange(new string[] { "Admin", "Lecturer", "Staff", "Student" });
        }

        private void ManageUsersForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            List<Users> users = userController.GetAllUsers();
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = users;
            dgvUsers.Columns["Password"].Visible = false;  // Hide password column for security
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

            if (userController.AddUser(user))
            {
                MessageBox.Show("User added successfully.", "Success");
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Failed to add user. Please check inputs.", "Error");
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

            if (userController.UpdateUser(user))
            {
                MessageBox.Show("User updated successfully.", "Success");
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Failed to update user.", "Error");
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
                if (userController.DeleteUser(selectedUserId))
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                var user = dgvUsers.CurrentRow.DataBoundItem as Users;
                if (user != null)
                {
                    selectedUserId = user.UserID;
                    txtUsername.Text = user.Username;
                    txtPassword.Text = user.Password;
                    cmbRole.SelectedItem = user.Role;
                }
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

