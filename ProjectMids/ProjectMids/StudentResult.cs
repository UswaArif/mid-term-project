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
    public partial class StudentResult : Form
    {
        public StudentResult()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into StudentResult values (@StudentId, @AssessmentComponentId, @RubricMeasurementId, @EvaluationDate)", con);

            cmd.Parameters.AddWithValue("@StudentId", txtStudentId.Text);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", txtAssessmentComponentId.Text);
            cmd.Parameters.AddWithValue("@RubricMeasurementId", txtRubricMeasurementId.Text);
            cmd.Parameters.AddWithValue("@EvaluationDate", dateTimePicker1.Value);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully saved");
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.gvStudentResult.SelectedRows)
            {
                gvStudentResult.Rows.RemoveAt(item.Index);
            }
            MessageBox.Show("Successfully Deleted");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * From StudentResult WHERE StudentId LIKE @StudentId + '%'", con);
            cmd.Parameters.AddWithValue("@StudentId", txtStudentId.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudentResult.DataSource = dt;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Searched");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Update StudentResult SET StudentId = @StudentId, AssessmentComponentId = @AssessmentComponentId, RubricMeasurementId = @RubricMeasurementId, EvaluationDate = @EvaluationDate WHERE @StudentId = StudentId", con);
            cmd.Parameters.AddWithValue("@StudentId", txtStudentId.Text);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", txtAssessmentComponentId.Text);
            cmd.Parameters.AddWithValue("@RubricMeasurementId", txtRubricMeasurementId.Text);
            cmd.Parameters.AddWithValue("@EvaluationDate", dateTimePicker1.Value);


            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully updated");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from StudentResult", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudentResult.DataSource = dt;
        }

        private void gvStudentResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvStudentResult.Rows[e.RowIndex];

                txtStudentId.Text = row.Cells["StudentId"].Value.ToString();
                txtAssessmentComponentId.Text = row.Cells["AssessmentComponentId"].Value.ToString();
                txtRubricMeasurementId.Text = row.Cells["RubricMeasurement"].Value.ToString();
                dateTimePicker1.Text = row.Cells["EvaluationDate"].Value.ToString();

            }
        }

        private void txtStudentId_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                e.Cancel = true;
                txtStudentId.Focus();
                errorProviderApp.SetError(txtStudentId, "Student Id should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtStudentId, "");
            }
        }

        private void txtAssessmentComponentId_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAssessmentComponentId.Text))
            {
                e.Cancel = true;
                txtAssessmentComponentId.Focus();
                errorProviderApp.SetError(txtAssessmentComponentId, "Assessment Component Id should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtAssessmentComponentId, "");
            }
        }

        private void txtRubricMeasurementId_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRubricMeasurementId.Text))
            {
                e.Cancel = true;
                txtRubricMeasurementId.Focus();
                errorProviderApp.SetError(txtRubricMeasurementId, "Rubric Measurement Id should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtRubricMeasurementId, "");
            }
        }

        private void txtStudentId_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAssessmentComponentId_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtRubricMeasurementId_KeyPress(object sender, KeyPressEventArgs e)
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

        }
    }
}
