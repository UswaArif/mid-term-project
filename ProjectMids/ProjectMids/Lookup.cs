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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) == false)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * From Lookup WHERE Name LIKE @Name", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvLookup.DataSource = dt;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Searched");
            }
            else
            {
                MessageBox.Show("Enter Name to Search.");
            }
                
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
            }
        }

        private void lblStudentMenu_Click(object sender, EventArgs e)
        {

        }
    }
}
