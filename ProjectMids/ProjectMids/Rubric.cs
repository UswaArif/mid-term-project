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
    public partial class Rubric : Form
    {
        public Rubric()
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
                SqlCommand cmd = new SqlCommand("Insert into Rubric values (@Id, @Details, @CloId)", con);

                cmd.Parameters.AddWithValue("@Id", txtID.Text);
                cmd.Parameters.AddWithValue("@Details", txtDetails.Text);
                cmd.Parameters.AddWithValue("@CloId", txtCLOID.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.gvRubric.SelectedRows)
            {
                gvRubric.Rows.RemoveAt(item.Index);
            }
            MessageBox.Show("Successfully Deleted");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * From Rubric WHERE Id LIKE @Id + '%'", con);
            cmd.Parameters.AddWithValue("@Id", txtID.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvRubric.DataSource = dt;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Searched");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Update Rubric SET Id = @Id, Details = @Details, CloId= @CloId WHERE @Id = Id", con);
                cmd.Parameters.AddWithValue("@Id", txtID.Text);
                cmd.Parameters.AddWithValue("@Details", txtDetails.Text);
                cmd.Parameters.AddWithValue("@CloId", txtCLOID.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated");
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvRubric.DataSource = dt;
        }

        private void gvRubric_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvRubric.Rows[e.RowIndex];

                txtID.Text = row.Cells["Id"].Value.ToString();
                txtDetails.Text = row.Cells["Details"].Value.ToString();
                txtCLOID.Text = row.Cells["CloId"].Value.ToString();
            }
        }

        private void txtID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                e.Cancel = true;
                txtID.Focus();
                errorProviderApp.SetError(txtID, "ID should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtID, "");
            }
        }

        private void txtDetails_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDetails.Text))
            {
                e.Cancel = true;
                txtDetails.Focus();
                errorProviderApp.SetError(txtDetails, "Details should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtDetails, "");
            }
        }

        private void txtCLOID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCLOID.Text))
            {
                e.Cancel = true;
                txtCLOID.Focus();
                errorProviderApp.SetError(txtCLOID, "CloId should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtCLOID, "");
            }
        }

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

        private void txtCLOID_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Rubric_Load(object sender, EventArgs e)
        {

        }
    }
}
