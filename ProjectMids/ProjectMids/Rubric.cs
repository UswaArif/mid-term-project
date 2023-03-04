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
        public static List<int> cloId = new List<int>();
        public Rubric()
        {
            InitializeComponent();
        }

        public static void GetCloIdData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id from Clo", con);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                cloId.Add((int)reader["Id"]);
            }
            reader.Close();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled) )
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd1 = new SqlCommand("Select COUNT(*) From Rubric where Id = @Id", con);
                cmd1.Parameters.AddWithValue("@Id", txtID.Text);
                int RubricIdCount = (int)cmd1.ExecuteScalar();

                if (RubricIdCount > 0)
                {
                    // Display error message if CLO ID is not present in the CLO database
                    MessageBox.Show("This Rubric Id has already present!");
                }

                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into Rubric values (@Id, @Details, @CloId)", con);

                    cmd.Parameters.AddWithValue("@Id", txtID.Text);
                    cmd.Parameters.AddWithValue("@Details", txtDetails.Text);
                    cmd.Parameters.AddWithValue("@CloId", cboCLoId.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }
            }           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*foreach (DataGridViewRow item in this.gvRubric.SelectedRows)
            {
                gvRubric.Rows.RemoveAt(item.Index);
            }
            MessageBox.Show("Successfully Deleted");*/
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) == false)
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
            else
            {
                MessageBox.Show("Enter Rubric Id to Search.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                var con = Configuration.getInstance().getConnection();

                SqlCommand cmd = new SqlCommand("Update Rubric SET Id = @Id, Details = @Details, CloId= @CloId WHERE @Id = Id", con);
                cmd.Parameters.AddWithValue("@Id", txtID.Text);
                cmd.Parameters.AddWithValue("@Details", txtDetails.Text);
                cmd.Parameters.AddWithValue("@CloId", cboCLoId.Text);

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
                cboCLoId.Text = row.Cells["CloId"].Value.ToString();
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

                foreach (Control c in tableLayoutPanel2.Controls)
                {
                    if (c is ComboBox)
                    {
                        ((ComboBox)c).Text = "";
                    }
                }
                cboCLoId.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Rubric_Load(object sender, EventArgs e)
        {
            GetCloIdData();

            cboCLoId.Text = "Select CLO Id";
            cboCLoId.DataSource = cloId;

            List<object> uniqueItems = new List<object>();
            foreach (object item in cboCLoId.Items)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }

            cboCLoId.DataSource = new BindingSource(uniqueItems, null);
        }

    }
}
