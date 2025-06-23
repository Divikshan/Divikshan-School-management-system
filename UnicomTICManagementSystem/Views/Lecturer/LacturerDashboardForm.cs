using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;


namespace UnicomTICManagementSystem.Views
{
    public partial class LecturerDashboardForm : Form
    {
        private readonly Lecturer _lecturer;
        private readonly SQLiteConnection _connection;

        public LecturerDashboardForm(Lecturer lecturer, SQLiteConnection connection)
        {
            InitializeComponent();
            _lecturer = lecturer;
            _connection = connection;

            btnBack.Click += btnBack_Click;
            btnUpdateMark.Click += btnUpdateMark_Click;

            this.Load += LecturerDashboardForm_Load;  // Hook Load event here
        }

        private void LecturerDashboardForm_Load(object sender, EventArgs e)
        {
            LoadMarks();
            LoadTimetable();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }

        private void LoadMarks()
        {
            var marksRepo = new MarksRepository(_connection);
            List<Marks> marks = marksRepo.GetMarksByLecturerId(_lecturer.LecturerID);
            dgvMarks.DataSource = marks;
        }

        private void LoadTimetable()
        {
            var timetableRepo = new TimetableRepository(_connection);
            List<Timetable> timetable = timetableRepo.GetTimetableByLecturerId(_lecturer.LecturerID);
            dgvTimetable.DataSource = timetable;
        }

        private void btnUpdateMark_Click(object sender, EventArgs e)
        {
            if (dgvMarks.CurrentRow != null)
            {
                // Use exact column name as in your data source; adjust if needed
                int markId = Convert.ToInt32(dgvMarks.CurrentRow.Cells["MarkID"].Value);

                int newMark;
                if (int.TryParse(txtNewMark.Text.Trim(), out newMark))
                {
                    var marksRepo = new MarksRepository(_connection);
                    marksRepo.UpdateMark(markId, newMark); // assuming this method exists as such
                    MessageBox.Show("Mark updated successfully.");
                    LoadMarks();
                }
                else
                {
                    MessageBox.Show("Please enter a valid mark.");
                }
            }
        }
    }
}
