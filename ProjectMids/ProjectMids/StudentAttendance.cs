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
            /*var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values (@AttendanceId,@StudentId, @AttendanceStatus)", con);
            cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);
            //cmd.Parameters.AddWithValue("@AttendanceId", txtAttendanceId.Text);
            cmd.Parameters.AddWithValue("@AttendanceStatus", cboAttendanceId.Text);
           
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully saved");*/
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*ar con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM StudentAttendance WHERE @StudentId = StudentId", con);
            cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Deleted");*/
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            /*var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * From StudentAttendance WHERE StudentId LIKE @StudentId + '%'", con);
            cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudentAttendance.DataSource = dt;
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
            SqlCommand cmd = new SqlCommand("Select * from Student where Status != 6", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudentAttendance.DataSource = dt;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in tableLayoutPanel1.Controls)
                {
                    if (c is TextBox)
                        ((TextBox)c).Clear();
                }

                foreach (Control c in tableLayoutPanel1.Controls)
                {
                    if(c is ComboBox)
                    {
                        ((ComboBox)c).Text = "";
                    }
                }
                cboAttendanceId.Text = "Select Attendance Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StudentAttendance_Load(object sender, EventArgs e)
        {
            ClassAttendance.GetUsersData();

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

            //ClassAttendance.classId.RemoveAll(x => ClassAttendance.classId.Count(y => x == y) == 1);
            //string[] s = new string[] { "ali", "amna", "uswa" };
            /*foreach(int c in ClassAttendance.classId)
            {
                cboAttendanceId.Items.AddRange();
            }*/

            /*var con = Configuration.getInstance().getConnection();
            try
            {
                string query = "select StudentId from Student";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "Student");
                //cboStatus.DisplayMember = "StudentId";
                cboAtStatus.ValueMember = "StudentId";
                cboAtStatus.DataSource = ds.Tables["Student"];
            }
            catch (Exception ex)
            {
                // write exception info to log or anything else
                MessageBox.Show("Error occured!");
            }*/
        }

        private void gvStudentAttendance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvStudentAttendance.Rows[e.RowIndex];

                txtStudentID.Text = row.Cells["Id"].Value.ToString();
            }

            /*if (gvStudentAttendance.Columns["Present"].Index == e.ColumnIndex)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values (@AttendanceId, @StudentId, @AttendanceStatus)", con);
                cmd.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);

                cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);
                cmd.Parameters.AddWithValue("@AttendanceStatus", 1);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }*/
            if (gvStudentAttendance.Columns["Present"].Index == e.ColumnIndex)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd1 = new SqlCommand("Select COUNT(*) from StudentAttendance where AttendanceId = @AttendanceId AND StudentId = @StudentId", con);
                cmd1.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);
                cmd1.Parameters.AddWithValue("@StudentId", txtStudentID.Text);

                int count = (int)cmd1.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("This Student Id has already been marked for this attendance!");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values (@AttendanceId, @StudentId, @AttendanceStatus)", con);
                    cmd.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);
                    cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);
                    cmd.Parameters.AddWithValue("@AttendanceStatus", 1);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }
            }

            else if (gvStudentAttendance.Columns["Absent"].Index == e.ColumnIndex)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM StudentAttendance WHERE AttendanceId = @AttendanceId AND StudentId = @StudentId", con);
                cmd2.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);
                cmd2.Parameters.AddWithValue("@StudentId", txtStudentID.Text);

                int count = (int)cmd2.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("This student has already been marked for this attendance!");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values (@AttendanceId, @StudentId, @AttendanceStatus)", con);
                    cmd.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);
                    cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);
                    cmd.Parameters.AddWithValue("@AttendanceStatus", 2);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }
            }

            else if (gvStudentAttendance.Columns["Late"].Index == e.ColumnIndex)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM StudentAttendance WHERE AttendanceId = @AttendanceId AND StudentId = @StudentId", con);
                cmd3.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);
                cmd3.Parameters.AddWithValue("@StudentId", txtStudentID.Text);

                int count = (int)cmd3.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("This student has already been marked for this attendance!");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values (@AttendanceId, @StudentId, @AttendanceStatus)", con);
                    cmd.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);
                    cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);
                    cmd.Parameters.AddWithValue("@AttendanceStatus", 4);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }
            }
            else
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM StudentAttendance WHERE AttendanceId = @AttendanceId AND StudentId = @StudentId", con);
                cmd4.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);
                cmd4.Parameters.AddWithValue("@StudentId", txtStudentID.Text);

                int count = (int)cmd4.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("This student has already been marked for this attendance!");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values (@AttendanceId, @StudentId, @AttendanceStatus)", con);
                    cmd.Parameters.AddWithValue("@AttendanceId", cboAttendanceId.Text);
                    cmd.Parameters.AddWithValue("@StudentId", txtStudentID.Text);
                    cmd.Parameters.AddWithValue("@AttendanceStatus", 3);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }
            }

            

        }
    }
}
