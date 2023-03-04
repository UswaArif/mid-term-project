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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Form activeForm = null;
        
        private void OpenChildForm(Form childfrom)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childfrom;
            childfrom.TopLevel = false;
            childfrom.FormBorderStyle = FormBorderStyle.None;
            childfrom.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childfrom);
            panelChildForm.Tag = childfrom;
            childfrom.BringToFront();
            childfrom.Show();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult iExit;
            try
            {
                iExit = MessageBox.Show("Confirm if you want to exit", "Student Result System",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(iExit == DialogResult.Yes)
                {
                    Application.Exit();
                }
                    
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        private void btnStudentAttendance_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StudentAttendance());
        }

        private void btnClassAttendance_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ClassAttendance());
        }

        private void btnLookUp_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Lookup());
        }

        private void btnCLO_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Clo());
        }

        private void btnRubric_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Rubric());
        }

        private void btnRubricLevel_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RubricLevel());
        }

        private void btnAssessment_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Assessment());
        }

        private void btnComponent_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AssessmentComponent());
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StudentResult());
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Student());
        }
    }
}
