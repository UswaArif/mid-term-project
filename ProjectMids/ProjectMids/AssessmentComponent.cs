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
    public partial class AssessmentComponent : Form
    {
        public static List<int> AssessmentId = new List<int>();
        public AssessmentComponent()
        {
            InitializeComponent();
        }

        public static void GetAssessmentIdData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id from Assessment", con);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                AssessmentId.Add((int)reader["Id"]);
            }
            reader.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into AssessmentComponent values (@Name, @RubricId, @TotalMarks, @DateCreated, @DateUpdated, @AssessmentId)", con);

            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@RubricId", cboRubricId.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", txtTotalMarks.Text);
                cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@AssessmentId", cboAssessmentId.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }

            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*foreach (DataGridViewRow item in this.gvAssessmentComponent.SelectedRows)
            {
                gvAssessmentComponent.Rows.RemoveAt(item.Index);
            }
            MessageBox.Show("Successfully Deleted");*/
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) == false)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * From AssessmentComponent WHERE Name LIKE @Name + '%'", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvAssessmentComponent.DataSource = dt;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Searched");
            }
            else
            {
                MessageBox.Show("Enter Name to Search.");
            }
                
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Update AssessmentComponent SET Name = @Name, RubricId = @RubricId, TotalMarks = @TotalMarks, DateCreated = @DateCreated, DateUpdated = @DateUpdated, AssessmentId = @AssessmentId WHERE @Id = Id", con);
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@RubricId", cboRubricId.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", txtTotalMarks.Text);
                cmd.Parameters.AddWithValue("@DateCreated", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@DateUpdated", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@AssessmentId", cboAssessmentId.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated");
            }

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvAssessmentComponent.DataSource = dt;
        }

        private void gvAssessmentComponent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvAssessmentComponent.Rows[e.RowIndex];

                txtId.Text = row.Cells["Id"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                cboRubricId.Text = row.Cells["RubricId"].Value.ToString();
                txtTotalMarks.Text = row.Cells["TotalMarks"].Value.ToString();
                dateTimePicker1.Text = row.Cells["DateCreated"].Value.ToString();
                dateTimePicker2.Text = row.Cells["DateUpdated"].Value.ToString();
                cboAssessmentId.Text = row.Cells["AssessmentId"].Value.ToString();
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

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
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
                }
                foreach (Control c in tableLayoutPanel2.Controls)
                {
                    if (c is ComboBox)
                    {
                        ((ComboBox)c).Text = "";
                    }
                }
                cboAssessmentId.Text = "1";
                cboRubricId.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AssessmentComponent_Load(object sender, EventArgs e)
        {
            GetAssessmentIdData();
            RubricLevel.GetRubricIdData();
            cboRubricId.DataSource = RubricLevel.rubricId;
            cboAssessmentId.DataSource = AssessmentId;

            List<object> uniqueItems = new List<object>();
            List<object> uniqueItems2 = new List<object>();
            foreach (object item in cboAssessmentId.Items)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }
            foreach (object item in cboRubricId.Items)
            {
                if (!uniqueItems2.Contains(item))
                {
                    uniqueItems2.Add(item);
                }
            }
            cboAssessmentId.DataSource = new BindingSource(uniqueItems, null);
            cboRubricId.DataSource = new BindingSource(uniqueItems2, null);
        }
    }
}
