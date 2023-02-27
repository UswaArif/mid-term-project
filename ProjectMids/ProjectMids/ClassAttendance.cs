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
    public partial class ClassAttendance : Form
    {
        public ClassAttendance()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * From ClassAttendance WHERE AttendanceDate LIKE @AttendanceDate", con);
            cmd.Parameters.AddWithValue("@RegistrationNumber", dateTimePicker1.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvClassAttendance.DataSource = dt;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Searched");
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into ClassAttendance values @AttendanceDate)", con);
            //string value = this.dateTimePicker1.Value.ToString("yyyyMMdd");
            cmd.Parameters.AddWithValue("@AttendanceDate", dateTimePicker1.Value);
            MessageBox.Show(dateTimePicker1.Value.ToString());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully saved");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM ClassAttendance WHERE @StudentId = StudentId", con);
            cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Deleted");*/
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Update ClassAttendance SET AttendanceDate = @AttendanceDate WHERE @AttendanceDate = AttendanceDate", con);
            cmd.Parameters.AddWithValue("@FirstName", dateTimePicker1.Value);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully updated");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from ClassAttendance", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvClassAttendance.DataSource = dt;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void ClassAttendance_Load(object sender, EventArgs e)
        {

        }
    }
}
