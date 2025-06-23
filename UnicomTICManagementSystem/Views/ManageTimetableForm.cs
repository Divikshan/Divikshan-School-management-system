using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Utilities;
using System.Data.SQLite;

namespace UnicomTICManagementSystem.Views
{
    public partial class ManageTimetableForm : Form
    {
        private TimetableController timetableController = new TimetableController();
        private List<Course> courses;
        private List<Subject> subjects;
        private List<Users> lecturers;
        private Form parentForm;

        public ManageTimetableForm(Form parent = null)
        {
            InitializeComponent();
            parentForm = parent;
        }


        public ManageTimetableForm()
        {
            InitializeComponent();
        }

        private void ManageTimetableForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadLecturers();
            LoadTimetables();
        }

        private void LoadCourses()
        {
            courses = new List<Course>();
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Courses";
                using (var cmd = new SQLiteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseID = Convert.ToInt32(reader["CourseID"]),
                            CourseName = reader["CourseName"].ToString()
                        });
                    }
                }
            }
            cmbCourse.DataSource = courses;
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseID";
            cmbCourse.SelectedIndex = -1;
        }

        private void LoadLecturers()
        {
            lecturers = new List<Users>();
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Role = 'Lecturer'";
                using (var cmd = new SQLiteCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lecturers.Add(new Users
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            Role = reader["Role"].ToString()
                        });
                    }
                }
            }
            cmbLecturer.DataSource = lecturers;
            cmbLecturer.DisplayMember = "Username";
            cmbLecturer.ValueMember = "UserID";
            cmbLecturer.SelectedIndex = -1;
        }

        private void LoadSubjects(int courseId)
        {
            subjects = new List<Subject>();
            using (var connection = DatabaseManager.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Subjects WHERE CourseID = @CourseID";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
                            {
                                SubjectID = Convert.ToInt32(reader["SubjectID"]),
                                SubjectName = reader["SubjectName"].ToString(),
                                CourseID = Convert.ToInt32(reader["CourseID"])
                            });
                        }
                    }
                }
            }
            cmbSubject.DataSource = subjects;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";
            cmbSubject.SelectedIndex = -1;
        }

        private void LoadTimetables()
        {
            var timetables = timetableController.GetAllTimetables();

            // For displaying related names, join with loaded courses, subjects, lecturers
            var table = new DataTable();
            table.Columns.Add("TimetableID", typeof(int));
            table.Columns.Add("Course", typeof(string));
            table.Columns.Add("Subject", typeof(string));
            table.Columns.Add("Lecturer", typeof(string));
            table.Columns.Add("Date", typeof(string));
            table.Columns.Add("TimeSlot", typeof(string));
            table.Columns.Add("Location", typeof(string));

            foreach (var t in timetables)
            {
                var courseName = courses.FirstOrDefault(c => c.CourseID == t.CourseID)?.CourseName ?? "";
                var subjectName = subjects.FirstOrDefault(s => s.SubjectID == t.SubjectID)?.SubjectName ?? "";
                var lecturerName = lecturers.FirstOrDefault(l => l.UserID == t.LecturerID)?.Username ?? "";

                table.Rows.Add(t.TimetableID, courseName, subjectName, lecturerName,
                    t.Date.ToString("yyyy-MM-dd"), t.TimeSlot, t.Location);
            }

            dgvTimetables.DataSource = table;

            dgvTimetables.Columns["TimetableID"].Visible = false; // hide PK
        }

        // When Course changes, reload Subjects for that Course
        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCourse.SelectedIndex >= 0)
            {
                int selectedCourseId = (int)cmbCourse.SelectedValue;
                LoadSubjects(selectedCourseId);
            }
            else
            {
                cmbSubject.DataSource = null;
            }
        }

        // Clear form inputs
        private void ClearForm()
        {
            cmbCourse.SelectedIndex = -1;
            cmbSubject.DataSource = null;
            cmbLecturer.SelectedIndex = -1;
            dtpDate.Value = DateTime.Today;
            txtTimeSlot.Clear();
            txtLocation.Clear();
            dgvTimetables.ClearSelection();
        }

        // Add new timetable entry
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                var timetable = new Timetable
                {
                    CourseID = (int)cmbCourse.SelectedValue,
                    SubjectID = (int)cmbSubject.SelectedValue,
                    LecturerID = (int)cmbLecturer.SelectedValue,
                    Date = dtpDate.Value.Date,
                    TimeSlot = txtTimeSlot.Text.Trim(),
                    Location = txtLocation.Text.Trim()
                };

                timetableController.AddTimetable(timetable);
                MessageBox.Show("Timetable added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTimetables();
                ClearForm();
            }
        }

        // Update selected timetable entry
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTimetables.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a timetable to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ValidateInputs())
            {
                int timetableId = Convert.ToInt32(dgvTimetables.SelectedRows[0].Cells["TimetableID"].Value);

                var timetable = new Timetable
                {
                    TimetableID = timetableId,
                    CourseID = (int)cmbCourse.SelectedValue,
                    SubjectID = (int)cmbSubject.SelectedValue,
                    LecturerID = (int)cmbLecturer.SelectedValue,
                    Date = dtpDate.Value.Date,
                    TimeSlot = txtTimeSlot.Text.Trim(),
                    Location = txtLocation.Text.Trim()
                };

                timetableController.UpdateTimetable(timetable);
                MessageBox.Show("Timetable updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTimetables();
                ClearForm();
            }
        }

        // Delete selected timetable entry
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTimetables.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a timetable to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int timetableId = Convert.ToInt32(dgvTimetables.SelectedRows[0].Cells["TimetableID"].Value);

            var confirm = MessageBox.Show("Are you sure you want to delete this timetable?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                timetableController.DeleteTimetable(timetableId);
                MessageBox.Show("Timetable deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTimetables();
                ClearForm();
            }
        }

        // Clear form inputs
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // When user selects a timetable row, populate form fields
        private void dgvTimetables_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTimetables.SelectedRows.Count == 0)
            {
                ClearForm();
                return;
            }

            var row = dgvTimetables.SelectedRows[0];

            // Set Course
            string courseName = row.Cells["Course"].Value.ToString();
            cmbCourse.SelectedIndex = courses.FindIndex(c => c.CourseName == courseName);

            // Load subjects for that course
            if (cmbCourse.SelectedIndex >= 0)
            {
                LoadSubjects(courses[cmbCourse.SelectedIndex].CourseID);
            }

            // Set Subject
            string subjectName = row.Cells["Subject"].Value.ToString();
            cmbSubject.SelectedIndex = subjects.FindIndex(s => s.SubjectName == subjectName);

            // Set Lecturer
            string lecturerName = row.Cells["Lecturer"].Value.ToString();
            cmbLecturer.SelectedIndex = lecturers.FindIndex(l => l.Username == lecturerName);

            // Set Date
            DateTime dateValue;
            if (DateTime.TryParse(row.Cells["Date"].Value.ToString(), out dateValue))
            {
                dtpDate.Value = dateValue;
            }

            // Set TimeSlot and Location
            txtTimeSlot.Text = row.Cells["TimeSlot"].Value.ToString();
            txtLocation.Text = row.Cells["Location"].Value.ToString();
        }

        // Validate form inputs
        private bool ValidateInputs()
        {
            if (cmbCourse.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a course.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbSubject.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a subject.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbLecturer.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a lecturer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTimeSlot.Text))
            {
                MessageBox.Show("Please enter a time slot.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter a location.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
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
