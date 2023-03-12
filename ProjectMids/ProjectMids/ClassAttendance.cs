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
        public static List<int> classId = new List<int>();
        //public static int[] classId = new int[] { };
        public ClassAttendance()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAttendanceId.Text) == false)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * From ClassAttendance WHERE Id LIKE @Id", con);
                cmd.Parameters.AddWithValue("@Id", txtAttendanceId.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvClassAttendance.DataSource = dt;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Searched");
            }
            else
            {
                MessageBox.Show("Enter Attendance Id to Search.");
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into ClassAttendance values (@AttendanceDate)", con);
            //string value = this.dateTimePicker1.Value.ToString("yyyyMMdd");
            //cmd.Parameters.AddWithValue("@Id", txtAttendanceId.Text);
            cmd.Parameters.AddWithValue("@AttendanceDate", dateTimePicker1.Value);
            MessageBox.Show(dateTimePicker1.Value.ToString());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully saved");

            
            //classId.Add()
        }

        public static void GetUsersData()
        {
   
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id from ClassAttendance", con);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                classId.Add((int)reader["Id"]);
            }
            reader.Close();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAttendanceId.Text) == false)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Update ClassAttendance SET AttendanceDate = @AttendanceDate WHERE @Id = Id", con);
                cmd.Parameters.AddWithValue("@Id", txtAttendanceId.Text);
                cmd.Parameters.AddWithValue("@AttendanceDate", dateTimePicker1.Value);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated");
            }
            else
            {
                MessageBox.Show("Enter Attendance Id to Update.");
            }
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
            this.Close();
        }

        private void ClassAttendance_Load(object sender, EventArgs e)
        {
            GetUsersData();
        }

        private void gvClassAttendance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvClassAttendance.Rows[e.RowIndex];

                txtAttendanceId.Text = row.Cells["Id"].Value.ToString();
                dateTimePicker1.Text = row.Cells["AttendanceDate"].Value.ToString();
            }
        }

        private void txtAttendanceId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == ((char)Keys.Back)) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in tableLayoutPanel2.Controls)
                {
                    if (c is TextBox)
                        ((TextBox)c).Clear();
                    else if (c is DateTimePicker)
                    {
                        ((DateTimePicker)c).Value = DateTime.Now; 
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
