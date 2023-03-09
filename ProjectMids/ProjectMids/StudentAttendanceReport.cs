using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;

namespace ProjectMids
{
    public partial class StudentAttendanceReport : Form
    {
        public StudentAttendanceReport()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select ca.Id as ClassAttendanceId,sa.StudentId,s.Name,s.RegistrationNumber,ca.AttendanceDate, CASE WHEN sa.AttendanceStatus = '1' THEN 'Present' WHEN sa.AttendanceStatus = '2' THEN 'Absent' WHEN sa.AttendanceStatus = '3' THEN 'Leave' Else 'Late' END as AttendanceStatus from ClassAttendance as ca INNER JOIN(Select AttendanceId, StudentId, AttendanceStatus from StudentAttendance) as sa ON sa.AttendanceId = ca.Id INNER JOIN (Select FirstName + ' ' + LastName as Name, Id, RegistrationNumber from Student) as s ON s.Id = sa.StudentId where sa.StudentId =@StudentId", con);
            cmd.Parameters.AddWithValue("@StudentId", cboStudentAttendance.Text);
            string Value = cboStudentAttendance.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudentAttendance.DataSource = dt;

            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            document.SetMargins(36f, 36f, 36f, 36f);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("StudentIdAttendanceReport.pdf", FileMode.Create));
            document.Open();
            Paragraph paragraph = new Paragraph("Student Attendance Report", FontFactory.GetFont("Times New Roman"));
            paragraph.Font.Size = 20;
            Paragraph paragraph1 = new Paragraph("Student Management System", FontFactory.GetFont("Times New Roman"));
            paragraph1.Font.Size = 22;
            Chunk para = new Chunk("            Student Id:" + Value, FontFactory.GetFont("Times New Roman"));
            para.Font.Size = 14;
            Paragraph paragraph2 = new Paragraph("                                 ");
            Paragraph paragraph3 = new Paragraph("                                 ");
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph1.Alignment = Element.ALIGN_CENTER;
            // Add the paragraph to the document
            document.Add(paragraph1);
            document.Add(paragraph);
            document.Add(paragraph2);
            document.Add(paragraph3);
            document.Add(para);
            PdfPTable table = new PdfPTable(gvStudentAttendance.Columns.Count);

            table.WidthPercentage = 85;

            // Add header column to the table
            foreach (DataGridViewColumn column in gvStudentAttendance.Columns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText));
                headerCell.BackgroundColor = BaseColor.GRAY; // Set the background color of the header cell
                table.AddCell(headerCell);
            }

            // Add data rows to the table
            foreach (DataGridViewRow row in gvStudentAttendance.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    PdfPCell dataCell = new PdfPCell(new Phrase(cell.Value?.ToString() ?? string.Empty));

                    if (cell.Style.BackColor != Color.Transparent && row.Index % 2 == 0) // Check if the cell has a background color
                    {
                        dataCell.BackgroundColor = BaseColor.LIGHT_GRAY; // Set the background color of the cell
                    }


                    table.AddCell(dataCell);
                }
            }

            document.Add(table);
            // Close the document
            document.Close();
        }

        private void StudentAttendanceReport_Load(object sender, EventArgs e)
        {
            StudentResult.GetStudentIdData();
            cboStudentAttendance.DataSource = StudentResult.studentId;
            List<object> uniqueStudentItems = new List<object>();
            foreach (object item in cboStudentAttendance.Items)
            {
                if (!uniqueStudentItems.Contains(item))
                {
                    uniqueStudentItems.Add(item);
                }
            }
            cboStudentAttendance.DataSource = new BindingSource(uniqueStudentItems, null);
        }
    }
}
