namespace UnicomTICManagementSystem.Views
{
    partial class StaffDashboardForm
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
            this.dgvTimetable = new System.Windows.Forms.DataGridView();
            this.dgvExams = new System.Windows.Forms.DataGridView();
            this.dgvMarks = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdateExam = new System.Windows.Forms.Button();
            this.btnUpdateMark = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtExamName = new System.Windows.Forms.TextBox();
            this.txtNewMark = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimetable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTimetable
            // 
            this.dgvTimetable.AllowUserToOrderColumns = true;
            this.dgvTimetable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimetable.Location = new System.Drawing.Point(460, 461);
            this.dgvTimetable.Name = "dgvTimetable";
            this.dgvTimetable.RowHeadersWidth = 51;
            this.dgvTimetable.RowTemplate.Height = 24;
            this.dgvTimetable.Size = new System.Drawing.Size(574, 278);
            this.dgvTimetable.TabIndex = 0;
            // 
            // dgvExams
            // 
            this.dgvExams.AllowUserToOrderColumns = true;
            this.dgvExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExams.Location = new System.Drawing.Point(21, 31);
            this.dgvExams.Name = "dgvExams";
            this.dgvExams.RowHeadersWidth = 51;
            this.dgvExams.RowTemplate.Height = 24;
            this.dgvExams.Size = new System.Drawing.Size(574, 278);
            this.dgvExams.TabIndex = 0;
            // 
            // dgvMarks
            // 
            this.dgvMarks.AllowUserToOrderColumns = true;
            this.dgvMarks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMarks.Location = new System.Drawing.Point(871, 31);
            this.dgvMarks.Name = "dgvMarks";
            this.dgvMarks.RowHeadersWidth = 51;
            this.dgvMarks.RowTemplate.Height = 24;
            this.dgvMarks.Size = new System.Drawing.Size(574, 278);
            this.dgvMarks.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 338);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Exam List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(868, 350);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Marks List";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(729, 767);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Timetable";
            // 
            // btnUpdateExam
            // 
            this.btnUpdateExam.Location = new System.Drawing.Point(447, 341);
            this.btnUpdateExam.Name = "btnUpdateExam";
            this.btnUpdateExam.Size = new System.Drawing.Size(148, 42);
            this.btnUpdateExam.TabIndex = 2;
            this.btnUpdateExam.Text = "Update Exam";
            this.btnUpdateExam.UseVisualStyleBackColor = true;
            // 
            // btnUpdateMark
            // 
            this.btnUpdateMark.Location = new System.Drawing.Point(1297, 341);
            this.btnUpdateMark.Name = "btnUpdateMark";
            this.btnUpdateMark.Size = new System.Drawing.Size(148, 42);
            this.btnUpdateMark.TabIndex = 2;
            this.btnUpdateMark.Text = "Update Marks";
            this.btnUpdateMark.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(1325, 723);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(120, 42);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // txtExamName
            // 
            this.txtExamName.Location = new System.Drawing.Point(130, 400);
            this.txtExamName.Name = "txtExamName";
            this.txtExamName.Size = new System.Drawing.Size(195, 22);
            this.txtExamName.TabIndex = 3;
            // 
            // txtNewMark
            // 
            this.txtNewMark.Location = new System.Drawing.Point(1040, 385);
            this.txtNewMark.Name = "txtNewMark";
            this.txtNewMark.Size = new System.Drawing.Size(195, 22);
            this.txtNewMark.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 406);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Exam Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(940, 391);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "New Marks";
            // 
            // StaffDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1474, 804);
            this.Controls.Add(this.txtNewMark);
            this.Controls.Add(this.txtExamName);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnUpdateMark);
            this.Controls.Add(this.btnUpdateExam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvMarks);
            this.Controls.Add(this.dgvExams);
            this.Controls.Add(this.dgvTimetable);
            this.Name = "StaffDashboardForm";
            this.Text = "StaffDashboardForm";
            this.Load += new System.EventHandler(this.StaffDashboardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimetable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTimetable;
        private System.Windows.Forms.DataGridView dgvExams;
        private System.Windows.Forms.DataGridView dgvMarks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdateExam;
        private System.Windows.Forms.Button btnUpdateMark;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtExamName;
        private System.Windows.Forms.TextBox txtNewMark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}