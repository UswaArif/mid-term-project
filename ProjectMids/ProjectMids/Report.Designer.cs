
namespace ProjectMids
{
    partial class Report
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
            this.btnStuAttendanceReport = new System.Windows.Forms.Button();
            this.gvStuAttendanceReport = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStudentManagement = new System.Windows.Forms.Label();
            this.lblSystem = new System.Windows.Forms.Label();
            this.lblStudentMenu = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblClassAttendance = new System.Windows.Forms.Label();
            this.cboClassAttendanceId = new System.Windows.Forms.ComboBox();
            this.btnAllClass = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvStuAttendanceReport)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStuAttendanceReport
            // 
            this.btnStuAttendanceReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStuAttendanceReport.Location = new System.Drawing.Point(451, 3);
            this.btnStuAttendanceReport.Name = "btnStuAttendanceReport";
            this.btnStuAttendanceReport.Size = new System.Drawing.Size(106, 52);
            this.btnStuAttendanceReport.TabIndex = 0;
            this.btnStuAttendanceReport.Text = "Generate Report";
            this.btnStuAttendanceReport.UseVisualStyleBackColor = true;
            this.btnStuAttendanceReport.Click += new System.EventHandler(this.btnStuAttendanceReport_Click);
            // 
            // gvStuAttendanceReport
            // 
            this.gvStuAttendanceReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvStuAttendanceReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStuAttendanceReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvStuAttendanceReport.Location = new System.Drawing.Point(0, 0);
            this.gvStuAttendanceReport.Name = "gvStuAttendanceReport";
            this.gvStuAttendanceReport.Size = new System.Drawing.Size(562, 245);
            this.gvStuAttendanceReport.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblStudentMenu);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 55);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.lblStudentManagement);
            this.panel2.Controls.Add(this.lblSystem);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(-1, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(121, 394);
            this.panel2.TabIndex = 5;
            // 
            // lblStudentManagement
            // 
            this.lblStudentManagement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStudentManagement.AutoSize = true;
            this.lblStudentManagement.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblStudentManagement.Location = new System.Drawing.Point(6, 173);
            this.lblStudentManagement.Name = "lblStudentManagement";
            this.lblStudentManagement.Size = new System.Drawing.Size(90, 110);
            this.lblStudentManagement.TabIndex = 5;
            this.lblStudentManagement.Text = "Student \r\nResult \r\nManage-\r\nment \r\nSystem";
            // 
            // lblSystem
            // 
            this.lblSystem.AutoSize = true;
            this.lblSystem.Location = new System.Drawing.Point(26, 215);
            this.lblSystem.Name = "lblSystem";
            this.lblSystem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblSystem.Size = new System.Drawing.Size(0, 13);
            this.lblSystem.TabIndex = 5;
            // 
            // lblStudentMenu
            // 
            this.lblStudentMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblStudentMenu.AutoSize = true;
            this.lblStudentMenu.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblStudentMenu.Location = new System.Drawing.Point(173, 7);
            this.lblStudentMenu.Name = "lblStudentMenu";
            this.lblStudentMenu.Size = new System.Drawing.Size(354, 36);
            this.lblStudentMenu.TabIndex = 0;
            this.lblStudentMenu.Text = "Class Attendance Report";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.gvStuAttendanceReport);
            this.panel3.Location = new System.Drawing.Point(61, 137);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(562, 245);
            this.panel3.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnStuAttendanceReport, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAllClass, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(124, 396);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 58);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.40093F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.31935F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.lblClassAttendance, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboClassAttendanceId, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(129, 75);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(429, 42);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // lblClassAttendance
            // 
            this.lblClassAttendance.AutoSize = true;
            this.lblClassAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClassAttendance.Location = new System.Drawing.Point(3, 0);
            this.lblClassAttendance.Name = "lblClassAttendance";
            this.lblClassAttendance.Size = new System.Drawing.Size(133, 42);
            this.lblClassAttendance.TabIndex = 0;
            this.lblClassAttendance.Text = "Class Attendance Id:";
            // 
            // cboClassAttendanceId
            // 
            this.cboClassAttendanceId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboClassAttendanceId.FormattingEnabled = true;
            this.cboClassAttendanceId.Location = new System.Drawing.Point(142, 3);
            this.cboClassAttendanceId.Name = "cboClassAttendanceId";
            this.cboClassAttendanceId.Size = new System.Drawing.Size(197, 21);
            this.cboClassAttendanceId.TabIndex = 1;
            // 
            // btnAllClass
            // 
            this.btnAllClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAllClass.Location = new System.Drawing.Point(339, 3);
            this.btnAllClass.Name = "btnAllClass";
            this.btnAllClass.Size = new System.Drawing.Size(106, 52);
            this.btnAllClass.TabIndex = 1;
            this.btnAllClass.Text = "Generate All Class Report";
            this.btnAllClass.UseVisualStyleBackColor = true;
            this.btnAllClass.Click += new System.EventHandler(this.btnAllClass_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox3.Location = new System.Drawing.Point(10, 297);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(72, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.LightGray;
            this.pictureBox2.Location = new System.Drawing.Point(3, 15);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(115, 116);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 461);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Report";
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvStuAttendanceReport)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStuAttendanceReport;
        private System.Windows.Forms.DataGridView gvStuAttendanceReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblStudentManagement;
        private System.Windows.Forms.Label lblSystem;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblStudentMenu;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblClassAttendance;
        private System.Windows.Forms.ComboBox cboClassAttendanceId;
        private System.Windows.Forms.Button btnAllClass;
    }
}