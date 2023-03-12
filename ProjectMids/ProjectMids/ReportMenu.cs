using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectMids
{
    public partial class ReportMenu : Form
    {
        public ReportMenu()
        {
            InitializeComponent();
        }

        private void btnClassAttendance_Click(object sender, EventArgs e)
        {
            Form classattend = new Report();
            classattend.ShowDialog();
        }

        private void btnCLowiseReport_Click(object sender, EventArgs e)
        {
            Form clo = new CLOWiseReport();
            clo.ShowDialog();
        }

        private void btnAssessment_Click(object sender, EventArgs e)
        {
            Form assessment = new AssessmentReport();
            assessment.ShowDialog();
        }

        private void btnStudentResult_Click(object sender, EventArgs e)
        {
            Form stuReslt = new StudentResultReport();
            stuReslt.ShowDialog();
        }

        private void btnStudentAttendance_Click(object sender, EventArgs e)
        {
            Form stuAttendance = new StudentAttendanceReport();
            stuAttendance.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
