using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class ExamForm : Form
    {
        private ExamController examController;
        private List<Subject> subjects;
        private int selectedExamId = -1;
        private Form parentForm;

        public ExamForm(Form parent = null)
        {
            InitializeComponent();
            parentForm = parent;
            examController = new ExamController();
        }

        private void ExamForm_Load(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadExams();
        }

        private void LoadSubjects()
        {
            subjects = examController.GetAllSubjects();
            cmbSubject.DataSource = subjects;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";
        }

        private void LoadExams()
        {
            dgvExams.DataSource = null;
            dgvExams.DataSource = examController.GetAllExams();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbSubject.SelectedItem == null || string.IsNullOrWhiteSpace(txtExamName.Text))
            {
                MessageBox.Show("Please select a subject and enter an exam name.");
                return;
            }

            var exam = new Exam
            {
                ExamName = txtExamName.Text.Trim(),
                SubjectID = ((Subject)cmbSubject.SelectedItem).SubjectID
            };

            examController.AddExam(exam);
            MessageBox.Show("Exam added successfully.");
            txtExamName.Clear();
            LoadExams();
        }

        private void dgvExams_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvExams.Rows[e.RowIndex];
                selectedExamId = Convert.ToInt32(row.Cells["ExamID"].Value);
                txtExamName.Text = row.Cells["ExamName"].Value.ToString();

                int subjectId = Convert.ToInt32(row.Cells["SubjectID"].Value);
                cmbSubject.SelectedValue = subjectId;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedExamId == -1)
            {
                MessageBox.Show("Please select an exam to update.");
                return;
            }

            var updatedExam = new Exam
            {
                ExamID = selectedExamId,
                ExamName = txtExamName.Text.Trim(),
                SubjectID = ((Subject)cmbSubject.SelectedItem).SubjectID
            };

            examController.UpdateExam(updatedExam);
            MessageBox.Show("Exam updated.");
            LoadExams();
            txtExamName.Clear();
            selectedExamId = -1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedExamId == -1)
            {
                MessageBox.Show("Please select an exam to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this exam?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    examController.DeleteExam(selectedExamId);
                    MessageBox.Show("Exam deleted.");
                    LoadExams();
                    txtExamName.Clear();
                    selectedExamId = -1;
                }
                catch
                {
                    MessageBox.Show("Delete failed.");
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            parentForm?.Show();
        }
    }
}
