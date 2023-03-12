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
    public partial class Clo : Form
    {
        
        public Clo()
        {
            InitializeComponent();
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Clo values (@Name, @DateCreated, @DateUpdated)", con);

                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            
        }      

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) == false)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * From Clo WHERE Name LIKE @Name + '%'", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCLO.DataSource = dt;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Searched");
            }
            else
            {
                MessageBox.Show("Enter CLO Name to Search.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if(string.IsNullOrEmpty(txtID.Text) == false)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Update Clo SET Name = @Name, DateCreated = @DateCreated, DateUpdated = @DateUpdated WHERE @Id = Id", con);
                    cmd.Parameters.AddWithValue("@Id", txtID.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated");
                }
                else
                {
                    MessageBox.Show("Enter CLO Id to Update.");
                }

            }
            
            
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvCLO.DataSource = dt;
        }

        private void gvCLO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvCLO.Rows[e.RowIndex];

                txtID.Text = row.Cells["Id"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                dateTimePicker1.Text = row.Cells["DateCreated"].Value.ToString();
                dateTimePicker2.Text = row.Cells["DateUpdated"].Value.ToString();
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                e.Cancel = true;
                txtName.Focus();
                errorProviderApp.SetError(txtName, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtName, "");
            }
        }

        /*private void txtID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                e.Cancel = true;
                txtID.Focus();
                errorProviderApp.SetError(txtID, "Id should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtID, "");
            }
        }*/

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Clo_Load(object sender, EventArgs e)
        {
            
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
