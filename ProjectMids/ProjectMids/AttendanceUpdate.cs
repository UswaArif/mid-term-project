using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjectMids
{
    public partial class AttendanceUpdate : Form
    {
        public AttendanceUpdate()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Update StudentAttendance SET AttendanceStatus = @AttendanceStatus WHERE @StudentId = StudentId and @AttendanceId = AttendanceId", con);
            cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);
            cmd.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);
            if(cboStatus.Text == "Present")
            {
                cmd.Parameters.AddWithValue("@AttendanceStatus", 1);
            }
            else if (cboStatus.Text == "Absent")
            {
                cmd.Parameters.AddWithValue("@AttendanceStatus", 2);
            }
            else if (cboStatus.Text == "Leave")
            {
                cmd.Parameters.AddWithValue("@AttendanceStatus", 3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AttendanceStatus", 4);
            }
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully updated");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from StudentAttendance", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void AttendanceUpdate_Load(object sender, EventArgs e)
        {
            ClassAttendance.GetUsersData();
            cboStatus.Text = "Present";
            cboAttendanceId.Text = "Select Attendance Id";
            cboAttendanceId.DataSource = ClassAttendance.classId;

            List<object> uniqueItems = new List<object>();
            foreach (object item in cboAttendanceId.Items)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }

            cboAttendanceId.DataSource = new BindingSource(uniqueItems, null);
        }
    }
}
