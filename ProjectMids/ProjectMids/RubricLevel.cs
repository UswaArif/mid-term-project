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
    public partial class RubricLevel : Form
    {
        public RubricLevel()
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
                SqlCommand cmd = new SqlCommand("Insert into RubricLevel values (@RubricId, @Details, @MeasurementLevel)", con);

                cmd.Parameters.AddWithValue("@RubricId", txtRubricId.Text);
                cmd.Parameters.AddWithValue("@Details", txtDetails.Text);
                cmd.Parameters.AddWithValue("@MeasurementLevel", txtMeasurementLevel.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.gvRubricLevel.SelectedRows)
            {
                gvRubricLevel.Rows.RemoveAt(item.Index);
            }
            MessageBox.Show("Successfully Deleted");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * From RubricLevel WHERE Id LIKE @Id + '%'", con);
            cmd.Parameters.AddWithValue("@Id", txtID.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvRubricLevel.DataSource = dt;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Searched");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Update RubricLevel SET RubricId = @RubricId, Details = @Details, MeasurementLevel = @MeasurementLevel WHERE @Id = Id", con);
                cmd.Parameters.AddWithValue("@Id", txtID.Text);
                cmd.Parameters.AddWithValue("@RubricId", txtRubricId.Text);
                cmd.Parameters.AddWithValue("@Details", txtDetails.Text);
                cmd.Parameters.AddWithValue("@MeasurementLevel", txtMeasurementLevel.Text);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated");
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from RubricLevel", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvRubricLevel.DataSource = dt;
        }

        private void gvRubricLevel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvRubricLevel.Rows[e.RowIndex];

                txtID.Text = row.Cells["Id"].Value.ToString();
                txtRubricId.Text = row.Cells["RubricId"].Value.ToString();
                txtDetails.Text = row.Cells["Details"].Value.ToString();
                txtMeasurementLevel.Text = row.Cells["MeasurementLevel"].Value.ToString();
            }
        }

        private void txtRubricId_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRubricId.Text))
            {
                e.Cancel = true;
                txtRubricId.Focus();
                errorProviderApp.SetError(txtRubricId, "Rubric Id should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtRubricId, "");
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

        private void txtMeasurementLevel_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMeasurementLevel.Text))
            {
                e.Cancel = true;
                txtMeasurementLevel.Focus();
                errorProviderApp.SetError(txtMeasurementLevel, "Measurement Level should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtMeasurementLevel, "");
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

        private void txtRubricId_KeyPress(object sender, KeyPressEventArgs e)
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
