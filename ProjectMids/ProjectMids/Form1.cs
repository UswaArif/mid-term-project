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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            Form student = new Student();
            student.ShowDialog();
        }

        private void btnStudentAttendance_Click(object sender, EventArgs e)
        {
            Form stuAttendance = new StudentAttendance();
            stuAttendance.ShowDialog();
        }

        private void btnClassAttendance_Click(object sender, EventArgs e)
        {
            Form ClassAttend = new ClassAttendance();
            ClassAttend.ShowDialog();
        }

        private void btnLookUp_Click(object sender, EventArgs e)
        {
            Form look = new Lookup();
            look.ShowDialog();
        }

        private void btnCLO_Click(object sender, EventArgs e)
        {
            Form clo = new Clo();
            clo.ShowDialog();
        }

        private void btnRubric_Click(object sender, EventArgs e)
        {
            Form rubric = new Rubric();
            rubric.ShowDialog();
        }

        private void btnRubricLevel_Click(object sender, EventArgs e)
        {
            Form rubriclevel = new RubricLevel();
            rubriclevel.ShowDialog();
        }

        private void btnAssessment_Click(object sender, EventArgs e)
        {
            Form assess = new Assessment();
            assess.ShowDialog();
        }

        private void btnComponent_Click(object sender, EventArgs e)
        {
            Form assComp = new AssessmentComponent();
            assComp.ShowDialog();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            Form stu = new StudentResult();
            stu.ShowDialog();
        }
    }
}
