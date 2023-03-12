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
    public partial class Assessment : Form
    {
        public Assessment()
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
                SqlCommand cmd = new SqlCommand("Insert into Assessment values (@Title, @DateCreated, @TotalMarks, @TotalWeightage)", con);

                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@TotalMarks", txtTotalMarks.Text);
                cmd.Parameters.AddWithValue("@TotalWeightage", txtWeightage.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
        }   

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) == false)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * From Assessment WHERE Title LIKE @Title + '%'", con);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvAssessment.DataSource = dt;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Searched");
            }

            else
            {
                MessageBox.Show("Enter Title to Search.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Update Assessment SET Title = @Title, DateCreated = @DateCreated, TotalMarks = @TotalMarks, TotalWeightage = @TotalWeightage WHERE @Id = Id", con);
                cmd.Parameters.AddWithValue("@Id", txtID.Text);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@TotalMarks", txtTotalMarks.Text);
                cmd.Parameters.AddWithValue("@TotalWeightage", txtWeightage.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated");
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvAssessment.DataSource = dt;
        }

        private void gvAssessment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvAssessment.Rows[e.RowIndex];

                txtID.Text = row.Cells["Id"].Value.ToString();
                txtTitle.Text = row.Cells["Title"].Value.ToString();
                dateTimePicker1.Text = row.Cells["DateCreated"].Value.ToString();
                txtTotalMarks.Text = row.Cells["TotalMarks"].Value.ToString();
                txtWeightage.Text= row.Cells["TotalWeightage"].Value.ToString();
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProviderApp.SetError(txtTitle, "Title should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtTitle, "");
            }
        }

        private void txtTotalMarks_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTotalMarks.Text))
            {
                e.Cancel = true;
                txtTotalMarks.Focus();
                errorProviderApp.SetError(txtTotalMarks, "Total Marks should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtTotalMarks, "");
            }
        }

        private void txtWeightage_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWeightage.Text))
            {
                e.Cancel = true;
                txtWeightage.Focus();
                errorProviderApp.SetError(txtWeightage, "Weightage should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtWeightage, "");
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

        private void txtTotalMarks_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtWeightage_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
