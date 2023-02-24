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
    }
}
