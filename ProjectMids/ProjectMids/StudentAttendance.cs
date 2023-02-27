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
    public partial class StudentAttendance : Form
    {
        public StudentAttendance()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values @StudentId,@AttendanceStatus)", con);
            cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);
            cmd.Parameters.AddWithValue("@AttendanceStatus", chkPresent.Text);
           
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully saved");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM StudentAttendance WHERE @StudentId = StudentId", con);
            cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Deleted");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            /*var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * From StudentAttendance WHERE @RegistrationNumber = RegistrationNumber", con);
            cmd.Parameters.AddWithValue("@RegistrationNumber", txtRegNo.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudent.DataSource = dt;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Searched");*/
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            /*var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Update StudentAttendance SET FirstName = @FirstName, LastName = @LastName, Contact = @Contact, Email = @Email, RegistrationNumber = @RegistrationNumber, Status = @Status WHERE @RegistrationNumber = RegistrationNumber", con);
            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully updated");*/
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from StudentAttendance", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudentAttendance.DataSource = dt;
        }

        private void chkPresent_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
