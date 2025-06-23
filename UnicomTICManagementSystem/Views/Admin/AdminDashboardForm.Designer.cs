namespace UnicomTICManagementSystem.Views.Admin
{
    partial class AdminDashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.BCourses = new System.Windows.Forms.Button();
            this.BSubject = new System.Windows.Forms.Button();
            this.BStudent = new System.Windows.Forms.Button();
            this.BExam = new System.Windows.Forms.Button();
            this.BMarks = new System.Windows.Forms.Button();
            this.BTimetable = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.BStaff = new System.Windows.Forms.Button();
            this.BLecturers = new System.Windows.Forms.Button();
            this.BRoom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(311, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "WelcomeAdmin";
            // 
            // BCourses
            // 
            this.BCourses.Location = new System.Drawing.Point(112, 169);
            this.BCourses.Name = "BCourses";
            this.BCourses.Size = new System.Drawing.Size(172, 60);
            this.BCourses.TabIndex = 1;
            this.BCourses.Text = "Manage Courses";
            this.BCourses.UseVisualStyleBackColor = true;
            this.BCourses.Click += new System.EventHandler(this.BCourses_Click);
            // 
            // BSubject
            // 
            this.BSubject.Location = new System.Drawing.Point(354, 169);
            this.BSubject.Name = "BSubject";
            this.BSubject.Size = new System.Drawing.Size(172, 60);
            this.BSubject.TabIndex = 1;
            this.BSubject.Text = "Manage Subject";
            this.BSubject.UseVisualStyleBackColor = true;
            this.BSubject.Click += new System.EventHandler(this.BSubjects_Click);
            // 
            // BStudent
            // 
            this.BStudent.Location = new System.Drawing.Point(599, 169);
            this.BStudent.Name = "BStudent";
            this.BStudent.Size = new System.Drawing.Size(172, 60);
            this.BStudent.TabIndex = 1;
            this.BStudent.Text = "Manage Student";
            this.BStudent.UseVisualStyleBackColor = true;
            this.BStudent.Click += new System.EventHandler(this.BStudents_Click);
            // 
            // BExam
            // 
            this.BExam.Location = new System.Drawing.Point(830, 169);
            this.BExam.Name = "BExam";
            this.BExam.Size = new System.Drawing.Size(172, 60);
            this.BExam.TabIndex = 1;
            this.BExam.Text = "Manage Exam";
            this.BExam.UseVisualStyleBackColor = true;
            this.BExam.Click += new System.EventHandler(this.BExams_Click);
            // 
            // BMarks
            // 
            this.BMarks.Location = new System.Drawing.Point(112, 305);
            this.BMarks.Name = "BMarks";
            this.BMarks.Size = new System.Drawing.Size(172, 60);
            this.BMarks.TabIndex = 1;
            this.BMarks.Text = "Manage Marks";
            this.BMarks.UseVisualStyleBackColor = true;
            this.BMarks.Click += new System.EventHandler(this.BMarks_Click_1);
            // 
            // BTimetable
            // 
            this.BTimetable.Location = new System.Drawing.Point(354, 305);
            this.BTimetable.Name = "BTimetable";
            this.BTimetable.Size = new System.Drawing.Size(172, 60);
            this.BTimetable.TabIndex = 1;
            this.BTimetable.Text = "Manage Timetable";
            this.BTimetable.UseVisualStyleBackColor = true;
            this.BTimetable.Click += new System.EventHandler(this.BTimetable_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(881, 512);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(121, 38);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.BLogout_Click);
            // 
            // BStaff
            // 
            this.BStaff.Location = new System.Drawing.Point(599, 305);
            this.BStaff.Name = "BStaff";
            this.BStaff.Size = new System.Drawing.Size(172, 60);
            this.BStaff.TabIndex = 1;
            this.BStaff.Text = "Manage Staff";
            this.BStaff.UseVisualStyleBackColor = true;
            this.BStaff.Click += new System.EventHandler(this.BStaff_Click);
            // 
            // BLecturers
            // 
            this.BLecturers.Location = new System.Drawing.Point(830, 305);
            this.BLecturers.Name = "BLecturers";
            this.BLecturers.Size = new System.Drawing.Size(172, 60);
            this.BLecturers.TabIndex = 1;
            this.BLecturers.Text = "Manage Lecturers";
            this.BLecturers.UseVisualStyleBackColor = true;
            this.BLecturers.Click += new System.EventHandler(this.BLecturers_Click);
            // 
            // BRoom
            // 
            this.BRoom.Location = new System.Drawing.Point(481, 444);
            this.BRoom.Name = "BRoom";
            this.BRoom.Size = new System.Drawing.Size(172, 60);
            this.BRoom.TabIndex = 1;
            this.BRoom.Text = "Manage Room";
            this.BRoom.UseVisualStyleBackColor = true;
            this.BRoom.Click += new System.EventHandler(this.BRoom_Click_1);
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1041, 571);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.BLecturers);
            this.Controls.Add(this.BStaff);
            this.Controls.Add(this.BTimetable);
            this.Controls.Add(this.BStudent);
            this.Controls.Add(this.BRoom);
            this.Controls.Add(this.BMarks);
            this.Controls.Add(this.BSubject);
            this.Controls.Add(this.BExam);
            this.Controls.Add(this.BCourses);
            this.Controls.Add(this.label1);
            this.Name = "AdminDashboardForm";
            this.Text = "AdminDashboardForm";
            this.Load += new System.EventHandler(this.AdminDashboardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BCourses;
        private System.Windows.Forms.Button BSubject;
        private System.Windows.Forms.Button BStudent;
        private System.Windows.Forms.Button BExam;
        private System.Windows.Forms.Button BMarks;
        private System.Windows.Forms.Button BTimetable;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button BStaff;
        private System.Windows.Forms.Button BLecturers;
        private System.Windows.Forms.Button BRoom;
    }
}