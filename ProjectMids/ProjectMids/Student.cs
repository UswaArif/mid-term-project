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
using System.Text.RegularExpressions;

namespace ProjectMids
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled) && ivalid_email(txtEmail.Text))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd1 = new SqlCommand("Select COUNT(*) from Student where RegistrationNumber = @RegistrationNumber", con);
                cmd1.Parameters.AddWithValue("@RegistrationNumber", txtRegNo.Text);

                int count = (int)cmd1.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("This Student Registration Number has already been present!");
                }

                else
                {
                    SqlCommand cmd = new SqlCommand("Insert into Student values (@FirstName, @LastName, @Contact, @Email, @RegistrationNumber, @Status)", con);
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@RegistrationNumber", txtRegNo.Text);
                    if(cboStatus.Text == "Active")
                    {
                        cmd.Parameters.AddWithValue("@Status", 5);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Status", 6);
                    }
                    

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }
            }
            
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudent.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE @RegistrationNumber = RegistrationNumber", con);
            cmd.Parameters.AddWithValue("@RegistrationNumber", txtRegNo.Text);
            
            cmd.ExecuteNonQuery();*/

            foreach (DataGridViewRow item in this.gvStudent.SelectedRows)
            {
                gvStudent.Rows.RemoveAt(item.Index);
            }
            MessageBox.Show("Successfully Deleted");
            //gvStudent.DataBind();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRegNo.Text)== false)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * From Student WHERE RegistrationNumber LIKE @RegistrationNumber + '%'", con);
                cmd.Parameters.AddWithValue("@RegistrationNumber", txtRegNo.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvStudent.DataSource = dt;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Searched");
            }

            else
            {
                MessageBox.Show("Enter Registration number to Search.");
            }
                
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled) && ivalid_email(txtEmail.Text))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Update Student SET FirstName = @FirstName, LastName = @LastName, Contact = @Contact, Email = @Email, RegistrationNumber = @RegistrationNumber, Status = @Status WHERE @RegistrationNumber = RegistrationNumber", con);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@RegistrationNumber", txtRegNo.Text);
                if (cboStatus.Text == "Active")
                {
                    cmd.Parameters.AddWithValue("@Status", 5);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Status", 6);
                }

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated");
            }
        }

        private void gvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gvStudent.Rows[e.RowIndex];

                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                txtContact.Text = row.Cells["Contact"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtRegNo.Text = row.Cells["RegistrationNumber"].Value.ToString();
                if(row.Cells["Status"].Value.ToString() == "5")
                {
                    cboStatus.Text = "Active";
                }
                else
                {
                    cboStatus.Text = "InActive";
                }
                //txtStatus.Text = row.Cells["Status"].Value.ToString();
            }
        }
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();
                errorProviderApp.SetError(txtFirstName, "First Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtFirstName, "");
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                e.Cancel = true;
                txtLastName.Focus();
                errorProviderApp.SetError(txtLastName, "Last Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtLastName, "");
            }
        }

        private void txtContact_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContact.Text))
            {
                e.Cancel = true;
                txtContact.Focus();
                errorProviderApp.SetError(txtContact, "Contact should not be left blank!");
            }
            else if (txtContact.Text.Length < 11 || txtContact.Text.Length > 11)
            {
                MessageBox.Show("Please enter a valid phone number with 11 digits.", "Invalid Phone Number",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtContact, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProviderApp.SetError(txtEmail, "Email should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtEmail, "");
            }
        }

        private void txtRegNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRegNo.Text))
            {
                e.Cancel = true;
                txtRegNo.Focus();
                errorProviderApp.SetError(txtRegNo, "Registration Number should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProviderApp.SetError(txtRegNo, "");
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Student_Load(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(Control c in tableLayoutPanel1.Controls)
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
                cboStatus.Text = "Active";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool ivalid_email(string email)
        {
            Regex check = new Regex(@"^\w+[\w-\.]+\@\w{5}\.[a-z]{2,3}$");
            bool valid = false;
            valid = check.IsMatch(email);
            if(valid == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Email format is incorrect.");
                return false;
            }
        }

    }
}
