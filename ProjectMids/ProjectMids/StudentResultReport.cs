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
    public partial class StudentResultReport : Form
    {
        public StudentResultReport()
        {
            InitializeComponent();
        }

        private void btnAlStudentReport_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT sr.StudentId, s.FirstName+' '+s.LastName AS Name, s.RegistrationNumber,  SUM(CAST(CAST(rl.MeasurementLevel AS DECIMAL(10, 2)) / 4 * CAST((ac.TotalMarks)AS DECIMAL(10, 2)) AS DECIMAL(10, 2))) AS ObtainedMarks, (SELECT SUM(TotalMarks) FROM Assessment) AS TotalMarks FROM StudentResult AS sr INNER JOIN ( SELECT Id, FirstName, LastName, RegistrationNumber FROM Student ) AS s ON sr.StudentId = s.Id INNER JOIN ( SELECT MeasurementLevel, Id FROM RubricLevel ) AS rl ON rl.Id = sr.RubricMeasurementId AND s.Id = sr.StudentId INNER JOIN ( SELECT Id, TotalMarks, Name, RubricId, AssessmentId FROM AssessmentComponent ) AS ac ON ac.Id = sr.AssessmentComponentId GROUP BY sr.StudentId, s.FirstName, s.LastName, s.RegistrationNumber ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudentResult.DataSource = dt;

            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            document.SetMargins(36f, 36f, 36f, 36f);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("ALLStudentResultReport.pdf", FileMode.Create));

            document.Open();
            Paragraph paragraph = new Paragraph("Student Result Report", FontFactory.GetFont("Times New Roman"));
            paragraph.Font.Size = 20;
            Paragraph paragraph1 = new Paragraph("Student Management System", FontFactory.GetFont("Times New Roman"));
            paragraph1.Font.Size = 22;
            Paragraph paragraph2 = new Paragraph("                                 ");
            Paragraph paragraph3 = new Paragraph("                                 ");
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph1.Alignment = Element.ALIGN_CENTER;
            // Add the paragraph to the document
            document.Add(paragraph1);
            document.Add(paragraph);
            document.Add(paragraph2);
            document.Add(paragraph3);


            PdfPTable table = new PdfPTable(gvStudentResult.Columns.Count);
            table.WidthPercentage = 100;

            // Add header column to the table
            foreach (DataGridViewColumn column in gvStudentResult.Columns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText));
                headerCell.BackgroundColor = BaseColor.GRAY; // Set the background color of the header cell
                table.AddCell(headerCell);
            }
            // Add data rows to the table
            foreach (DataGridViewRow row in gvStudentResult.Rows)
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

        private void StudentResultReport_Load(object sender, EventArgs e)
        {
            StudentResult.GetStudentIdData();
            cboStudentId.DataSource = StudentResult.studentId;
            List<object> uniqueStudentItems = new List<object>();
            foreach (object item in cboStudentId.Items)
            {
                if (!uniqueStudentItems.Contains(item))
                {
                    uniqueStudentItems.Add(item);
                }
            }
            cboStudentId.DataSource = new BindingSource(uniqueStudentItems, null);
        }

        private void btnStudentReport_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT sr.StudentId, s.FirstName+' '+s.LastName AS Name, s.RegistrationNumber,  SUM(CAST(CAST(rl.MeasurementLevel AS DECIMAL(10, 2)) / 4 * CAST((ac.TotalMarks)AS DECIMAL(10, 2)) AS DECIMAL(10, 2))) AS ObtainedMarks, (SELECT SUM(TotalMarks) FROM Assessment) AS TotalMarks FROM StudentResult AS sr INNER JOIN ( SELECT Id, FirstName, LastName, RegistrationNumber FROM Student ) AS s ON sr.StudentId = s.Id INNER JOIN ( SELECT MeasurementLevel, Id FROM RubricLevel ) AS rl ON rl.Id = sr.RubricMeasurementId AND s.Id = sr.StudentId INNER JOIN ( SELECT Id, TotalMarks, Name, RubricId, AssessmentId FROM AssessmentComponent ) AS ac ON ac.Id = sr.AssessmentComponentId where sr.StudentId = @Id GROUP BY sr.StudentId, s.FirstName, s.LastName, s.RegistrationNumber ", con);
            cmd.Parameters.AddWithValue("@Id", cboStudentId.Text);
            string Id = cboStudentId.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStudentResult.DataSource = dt;
            SqlDataReader dr = null;
            dr = cmd.ExecuteReader();
            string studentName = "";
            string reg = "";
            if(dr.Read())
            {
                studentName = dr.GetString(1);
                reg = dr.GetString(2);
            }

            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            document.SetMargins(36f, 36f, 36f, 36f);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("StudentResultReport.pdf", FileMode.Create));

            document.Open();
            Paragraph paragraph = new Paragraph("Student Result Report", FontFactory.GetFont("Times New Roman"));
            paragraph.Font.Size = 20;
            Paragraph paragraph1 = new Paragraph("Student Management System", FontFactory.GetFont("Times New Roman"));
            paragraph1.Font.Size = 22;
            Paragraph para = new Paragraph("Student Id :" + " " + Id, FontFactory.GetFont("Times New Roman"));
            para.Font.Size = 14;
            Paragraph para1 = new Paragraph("Student Name :" + " " + studentName, FontFactory.GetFont("Times New Roman"));;
            para.Font.Size = 14;
            Paragraph para2 = new Paragraph("Registration Number :"+ " "  + reg, FontFactory.GetFont("Times New Roman"));
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
            document.Add(para1);
            document.Add(para2);
            document.Add(paragraph2);
            document.Add(paragraph2);
            PdfPTable table = new PdfPTable(gvStudentResult.Columns.Count);
            table.WidthPercentage = 100;

            // Add header column to the table
            foreach (DataGridViewColumn column in gvStudentResult.Columns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText));
                headerCell.BackgroundColor = BaseColor.GRAY; // Set the background color of the header cell
                table.AddCell(headerCell);
            }
            // Add data rows to the table
            foreach (DataGridViewRow row in gvStudentResult.Rows)
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
    }
}
