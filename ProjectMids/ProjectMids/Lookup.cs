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
    public partial class Lookup : Form
    {
        public Lookup()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * From Lookup WHERE Name LIKE @Name", con);
            cmd.Parameters.AddWithValue("@RegistrationNumber", txtName.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvLookup.DataSource = dt;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Searched");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            /*var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Update Lookup SET Name = @First, LastName = @LastName, Contact = @Contact, Email = @Email, RegistrationNumber = @RegistrationNumber, Status = @Status WHERE @RegistrationNumber = RegistrationNumber", con);
            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
            cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@RegistrationNumber", txtRegNo.Text);
            cmd.Parameters.AddWithValue("@Status", txtStatus.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully updated");*/
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Lookup", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvLookup.DataSource = dt;
        }

        private void gvLookup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvLookup.Rows[e.RowIndex];

                txtName.Text = row.Cells["Name"].Value.ToString();
                txtCatageory.Text = row.Cells["Catageory"].Value.ToString();
            }
        }
    }
}
