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
        public static List<int> rubricId = new List<int>();
        public RubricLevel()
        {
            InitializeComponent();
        }

        public static void GetRubricIdData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id from Rubric", con);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                rubricId.Add((int)reader["Id"]);
            }
            reader.Close();

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

                cmd.Parameters.AddWithValue("@RubricId", cboRubricId.Text);
                cmd.Parameters.AddWithValue("@Details", txtDetails.Text);
                cmd.Parameters.AddWithValue("@MeasurementLevel", txtMeasurementLevel.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*foreach (DataGridViewRow item in this.gvRubricLevel.SelectedRows)
            {
                gvRubricLevel.Rows.RemoveAt(item.Index);
            }
            MessageBox.Show("Successfully Deleted");*/
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM RubricLevel WHERE @Id = Id", con);
            cmd.Parameters.AddWithValue("@Id", txtID.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Deleted");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) == false)
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

            else
            {
                MessageBox.Show("Enter Id to Search.");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Update RubricLevel SET RubricId = @RubricId, Details = @Details, MeasurementLevel = @MeasurementLevel WHERE @Id = Id", con);
                cmd.Parameters.AddWithValue("@Id", txtID.Text);
                cmd.Parameters.AddWithValue("@RubricId", cboRubricId.Text);
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
                cboRubricId.Text = row.Cells["RubricId"].Value.ToString();
                txtDetails.Text = row.Cells["Details"].Value.ToString();
                txtMeasurementLevel.Text = row.Cells["MeasurementLevel"].Value.ToString();
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in tableLayoutPanel2.Controls)
                {
                    if (c is TextBox)
                        ((TextBox)c).Clear();
                }

                foreach (Control c in tableLayoutPanel1.Controls)
                {
                    if (c is ComboBox)
                    {
                        ((ComboBox)c).Text = "";
                    }
                }
                cboRubricId.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RubricLevel_Load(object sender, EventArgs e)
        {
            GetRubricIdData();
            //cboAttendanceId.Text = "Select Rubric Id";
            cboRubricId.DataSource = rubricId;

            List<object> uniqueItems = new List<object>();
            foreach (object item in cboRubricId.Items)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }

            cboRubricId.DataSource = new BindingSource(uniqueItems, null);

        }
    }
}
