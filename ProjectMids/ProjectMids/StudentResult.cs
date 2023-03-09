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
        public static List<int> rubriclevelId = new List<int>();
        public static List<int> studentId = new List<int>();
        public static List<int> ComponentId = new List<int>();
        public StudentResult()
        {
            InitializeComponent();
        }

        public static void GetStudentIdData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id from Student where Status != 6", con);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                studentId.Add((int)reader["Id"]);
            }
            reader.Close();
        }

        public static void GetAssessmentComponentIdData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id from AssessmentComponent", con);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                ComponentId.Add((int)reader["Id"]);
            }
            reader.Close();
        }

        public static void GetRubricLevelIdData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id from RubricLevel", con);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                rubriclevelId.Add((int)reader["Id"]);
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
            SqlCommand cmd1 = new SqlCommand("Select COUNT(*) From StudentResult where StudentId = @StudentId AND AssessmentComponentId = @AssessmentComponentId AND RubricMeasurementId = @RubricMeasurementId", con);
            cmd1.Parameters.AddWithValue("@StudentId", cboStudentId.Text);
            cmd1.Parameters.AddWithValue("@AssessmentComponentId", cboComponentId.Text);
            cmd1.Parameters.AddWithValue("@RubricMeasurementId", cboRubricLevel.Text);
            int dataCount = (int)cmd1.ExecuteScalar();

            if (dataCount > 0)
            {
                MessageBox.Show("This data has already present!");
            }

            else
            {
                SqlCommand cmd = new SqlCommand("Insert into StudentResult values (@StudentId, @AssessmentComponentId, @RubricMeasurementId, @EvaluationDate)", con);

                cmd.Parameters.AddWithValue("@StudentId", cboStudentId.Text);
                cmd.Parameters.AddWithValue("@AssessmentComponentId", cboComponentId.Text);
                cmd.Parameters.AddWithValue("@RubricMeasurementId", cboRubricLevel.Text);
                cmd.Parameters.AddWithValue("@EvaluationDate", dateTimePicker1.Value);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }                     
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*foreach (DataGridViewRow item in this.gvStudentResult.SelectedRows)
            {
                gvStudentResult.Rows.RemoveAt(item.Index);
            }
            MessageBox.Show("Successfully Deleted");*/
            /*var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM StudentResult WHERE @StudentId = StudentId", con);
            cmd.Parameters.AddWithValue("@StudentId", cboStudentId.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Deleted");*/
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * From StudentResult WHERE StudentId LIKE @StudentId + '%'", con);
            cmd.Parameters.AddWithValue("@StudentId", cboStudentId.Text);
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
            cmd.Parameters.AddWithValue("@StudentId", cboStudentId.Text);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", cboComponentId.Text);
            cmd.Parameters.AddWithValue("@RubricMeasurementId", cboRubricLevel.Text);
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

                cboStudentId.Text = row.Cells["StudentId"].Value.ToString();
                cboComponentId.Text = row.Cells["AssessmentComponentId"].Value.ToString();
                cboRubricLevel.Text = row.Cells["RubricMeasurementId"].Value.ToString();
                dateTimePicker1.Text = row.Cells["EvaluationDate"].Value.ToString();
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in tableLayoutPanel1.Controls)
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
                cboRubricLevel.Text = "1";
                cboStudentId.Text = "1";
                cboComponentId.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StudentResult_Load(object sender, EventArgs e)
        {
            GetStudentIdData();
            GetRubricLevelIdData();
            GetAssessmentComponentIdData();

            cboStudentId.DataSource = studentId;
            cboRubricLevel.DataSource = rubriclevelId;
            cboComponentId.DataSource = ComponentId;

            List<object> uniqueStudentItems = new List<object>();
            List<object> uniqueItems = new List<object>();
            List<object> uniqueComponentId = new List<object>();

            foreach (object item in cboRubricLevel.Items)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }

            foreach (object item in cboStudentId.Items)
            {
                if (!uniqueStudentItems.Contains(item))
                {
                    uniqueStudentItems.Add(item);
                }
            }

            foreach (object item in cboComponentId.Items)
            {
                if (!uniqueComponentId.Contains(item))
                {
                    uniqueComponentId.Add(item);
                }
            }

            cboRubricLevel.DataSource = new BindingSource(uniqueItems, null);
            cboStudentId.DataSource = new BindingSource(uniqueStudentItems, null);
            cboComponentId.DataSource = new BindingSource(uniqueComponentId, null);

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

                //cboStudentId.Text = row.Cells["StudentId"].Value.ToString();
                //cboComponentId.Text = row.Cells["AssessmentComponentId"].Value.ToString();
                //cboRubricLevel.Text = row.Cells["RubricMeasurementId"].Value.ToString();
                //dateTimePicker1.Text = row.Cells["EvaluationDate"].Value.ToString();
            }
        }
    }
}
