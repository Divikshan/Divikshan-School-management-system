using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SQLite;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Views
{
    public partial class StaffDashboardForm : Form
    {
        private readonly Staff _staff;             // Added Staff object reference
        private readonly SQLiteConnection _connection;

        public StaffDashboardForm(Staff staff, SQLiteConnection connection)
        {
            InitializeComponent();
            _staff = staff;
            _connection = connection;

            btnBack.Click += btnBack_Click;
            btnUpdateExam.Click += btnUpdateExam_Click;
            btnUpdateMark.Click += btnUpdateMark_Click;

            this.Load += StaffDashboardForm_Load;
        }

        private void StaffDashboardForm_Load(object sender, EventArgs e)
        {
            LoadExams();
            LoadMarks();
            LoadTimetable();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }

        private void LoadExams()
        {
            var examRepo = new ExamRepository();   // No constructor params
            List<Exam> exams = examRepo.GetAllExams();
            dgvExams.DataSource = exams;
        }

        private void LoadMarks()
        {
            var marksRepo = new MarksRepository(_connection);
            List<Marks> marks = marksRepo.GetAllMarks();
            dgvMarks.DataSource = marks;
        }

        private void LoadTimetable()
        {
            var timetableRepo = new TimetableRepository(_connection);
            List<Timetable> timetable = timetableRepo.GetAllTimetables();
            dgvTimetable.DataSource = timetable;
        }

        private void btnUpdateExam_Click(object sender, EventArgs e)
        {
            if (dgvExams.CurrentRow != null)
            {
                int examId = Convert.ToInt32(dgvExams.CurrentRow.Cells["ExamID"].Value);
                string newName = txtExamName.Text.Trim();

                if (string.IsNullOrWhiteSpace(newName))
                {
                    MessageBox.Show("Please enter exam name.");
                    return;
                }

                var exam = new Exam
                {
                    ExamID = examId,
                    ExamName = newName
                    // No ExamDate property included
                };

                var examRepo = new ExamRepository();
                examRepo.UpdateExam(exam);  // void method, no bool returned
                MessageBox.Show("Exam updated successfully.");
                LoadExams();
            }
        }

        private void btnUpdateMark_Click(object sender, EventArgs e)
        {
            if (dgvMarks.CurrentRow != null)
            {
                int markId = Convert.ToInt32(dgvMarks.CurrentRow.Cells["MarkID"].Value);
                int newMark;

                if (int.TryParse(txtNewMark.Text.Trim(), out newMark))
                {
                    var mark = new Marks
                    {
                        MarkID = markId,
                        StudentID = Convert.ToInt32(dgvMarks.CurrentRow.Cells["StudentID"].Value),
                        SubjectID = Convert.ToInt32(dgvMarks.CurrentRow.Cells["SubjectID"].Value),
                        ExamID = Convert.ToInt32(dgvMarks.CurrentRow.Cells["ExamID"].Value),
                        MarksObtained = newMark
                    };

                    var marksRepo = new MarksRepository(_connection);
                    if (marksRepo.UpdateMark(mark))
                    {
                        MessageBox.Show("Mark updated successfully.");
                        LoadMarks();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update mark.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid mark.");
                }
            }
        }
    }
}
