using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Views
{
    public partial class StudentDashboardForm : Form
    {

       

        private readonly Students _student;
        private readonly SQLiteConnection _connection;

        public StudentDashboardForm(Students student, SQLiteConnection connection)
        {
            InitializeComponent();
            _student = student;
            _connection = connection;

            btnBack.Click += btnBack_Click;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();           // Close current StudentDashboardForm
            new LoginForm().Show(); // Show the LoginForm again
        }

        private void StudentDashboardForm_Load(object sender, EventArgs e)
        {
            LoadMarks();
            LoadTimetable();
            
        }

        private void LoadMarks()
        {
            var marksRepo = new MarksRepository(_connection);
            List<Marks> marks = marksRepo.GetMarksByStudentId(_student.StudentID);
            dgvMarks.DataSource = marks;
        }

        private void LoadTimetable()
        {
            var timetableRepo = new TimetableRepository(_connection);
            List<Timetable> timetable = timetableRepo.GetTimetableByCourseId(_student.CourseID);
            dgvTimetable.DataSource = timetable;
        }
    }
}
