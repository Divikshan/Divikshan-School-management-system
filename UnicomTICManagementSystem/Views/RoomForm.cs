using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Views
{
    public partial class RoomForm : Form
    {
        private RoomRepository roomRepo;
        private int selectedRoomId = -1;

        public RoomForm()  // ✅ FIXED: constructor name matches class name
        {
            InitializeComponent();
            roomRepo = new RoomRepository();
        }

        private void RoomForm_Load(object sender, EventArgs e)  // ✅ FIXED: Load event handler name
        {
            LoadRooms();
        }

        private void LoadRooms()
        {
            List<Room> rooms = roomRepo.GetAllRooms();
            dgvRooms.DataSource = rooms;

            dgvRooms.Columns["RoomID"].Visible = false; // Ensure column name matches property
            dgvRooms.ClearSelection();
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtRoomName.Text = "";
            cmbRoomType.SelectedIndex = -1;
            selectedRoomId = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtRoomName.Text.Trim();
            string type = cmbRoomType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Please fill in all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Room newRoom = new Room { RoomName = name, RoomType = type };
            bool success = roomRepo.AddRoom(newRoom);

            MessageBox.Show(success ? "Room added." : "Failed to add.", "Result",
                MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success) LoadRooms();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Select a room to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtRoomName.Text.Trim();
            string type = cmbRoomType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Please fill in all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Room updatedRoom = new Room { RoomID = selectedRoomId, RoomName = name, RoomType = type };
            bool success = roomRepo.UpdateRoom(updatedRoom);

            MessageBox.Show(success ? "Room updated." : "Update failed.", "Result",
                MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success) LoadRooms();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Select a room to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool success = roomRepo.DeleteRoom(selectedRoomId);
                MessageBox.Show(success ? "Room deleted." : "Delete failed.", "Result",
                    MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                if (success) LoadRooms();
            }
        }

        private void dgvRooms_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count > 0)
            {
                var row = dgvRooms.SelectedRows[0];
                selectedRoomId = Convert.ToInt32(row.Cells["RoomID"].Value);  // Use correct column name
                txtRoomName.Text = row.Cells["RoomName"].Value.ToString();
                cmbRoomType.SelectedItem = row.Cells["RoomType"].Value.ToString();
            }
            else
            {
                ClearInputs();
            }
        }
    }

}
