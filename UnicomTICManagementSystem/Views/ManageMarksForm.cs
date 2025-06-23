using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageMarksForm : Form
    {
        private Form parentForm;
        private MarksController marksController = new MarksController();
        private string connectionString = "Data Source=DBconnection.db";

        public ManageMarksForm(Form parent = null)
        {
            InitializeComponent();
            parentForm = parent;

            LoadStudents();
            LoadSubjects();
            LoadExams();
            LoadMarks();
        }

        private void ManageMarksForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
            LoadSubjects();
            LoadExams();
            LoadMarks();
        }

        private void LoadStudents()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT StudentId, StudentName FROM Students";
                using (var da = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbStudent.DataSource = dt;
                    cmbStudent.DisplayMember = "StudentName";
                    cmbStudent.ValueMember = "StudentId";
                }
            }
        }

        private void LoadSubjects()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT SubjectID, SubjectName FROM Subjects";
                using (var da = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbSubject.DataSource = dt;
                    cmbSubject.DisplayMember = "SubjectName";
                    cmbSubject.ValueMember = "SubjectID";
                }
            }
        }

        private void LoadExams()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ExamID, ExamName FROM Exams";
                using (var da = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbExam.DataSource = dt;
                    cmbExam.DisplayMember = "ExamName";
                    cmbExam.ValueMember = "ExamID";
                }
            }
        }

        private void LoadMarks()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        m.MarkID AS vMarkID, 
                        s.StudentId, s.StudentName, 
                        sub.SubjectID, sub.SubjectName, 
                        e.ExamID, e.ExamName, 
                        m.MarksObtained 
                    FROM Marks m
                    INNER JOIN Students s ON m.StudentID = s.StudentId
                    INNER JOIN Subjects sub ON m.SubjectID = sub.SubjectID
                    INNER JOIN Exams e ON m.ExamID = e.ExamID";

                using (var da = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvMarks.DataSource = dt;

                    // Hide ID columns
                    dgvMarks.Columns["StudentId"].Visible = false;
                    dgvMarks.Columns["SubjectID"].Visible = false;
                    dgvMarks.Columns["ExamID"].Visible = false;
                }
            }
        }

        private void ClearInputs()
        {
            if (cmbStudent.Items.Count > 0) cmbStudent.SelectedIndex = 0;
            if (cmbSubject.Items.Count > 0) cmbSubject.SelectedIndex = 0;
            if (cmbExam.Items.Count > 0) cmbExam.SelectedIndex = 0;
            txtMarks.Clear();
            dgvMarks.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            if (cmbStudent.SelectedValue == null || cmbSubject.SelectedValue == null || cmbExam.SelectedValue == null)
            {
                MessageBox.Show("Dropdowns are not fully loaded.");
                return;
            }

            var mark = new Marks
            {
                StudentID = Convert.ToInt32(cmbStudent.SelectedValue),
                SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                ExamID = Convert.ToInt32(cmbExam.SelectedValue),
                MarksObtained = Convert.ToInt32(txtMarks.Text)
            };

            marksController.AddMark(mark);
            LoadMarks();
            ClearInputs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count > 0 && ValidateInputs())
            {
                var mark = new Marks
                {
                    MarkID = Convert.ToInt32(dgvMarks.SelectedRows[0].Cells["vMarkID"].Value),
                    StudentID = Convert.ToInt32(cmbStudent.SelectedValue),
                    SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                    ExamID = Convert.ToInt32(cmbExam.SelectedValue),
                    MarksObtained = Convert.ToInt32(txtMarks.Text)
                };

                marksController.UpdateMark(mark);
                LoadMarks();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count > 0)
            {
                int markID = Convert.ToInt32(dgvMarks.SelectedRows[0].Cells["vMarkID"].Value);
                marksController.DeleteMark(markID);
                LoadMarks();
                ClearInputs();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void dgvMarks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMarks.SelectedRows.Count > 0)
            {
                var row = dgvMarks.SelectedRows[0];

                cmbStudent.SelectedValue = Convert.ToInt32(row.Cells["StudentId"].Value);
                cmbSubject.SelectedValue = Convert.ToInt32(row.Cells["SubjectID"].Value);
                cmbExam.SelectedValue = Convert.ToInt32(row.Cells["ExamID"].Value);
                txtMarks.Text = row.Cells["MarksObtained"].Value.ToString();
            }
        }

        private bool ValidateInputs()
        {
            int marks;
            if (cmbStudent.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a student.");
                return false;
            }
            if (cmbSubject.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a subject.");
                return false;
            }
            if (cmbExam.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an exam.");
                return false;
            }
            if (!int.TryParse(txtMarks.Text, out marks) || marks < 0)
            {
                MessageBox.Show("Please enter valid marks (0 or more).");
                return false;
            }
            return true;
        }

       
        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
            if (parentForm != null)
            {
                parentForm.Show();
            }

        }

       
    }
}
