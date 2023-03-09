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
    public partial class AssessmentReport : Form
    {
        public static List<string> AssessmentName = new List<string>();
        public AssessmentReport()
        {
            InitializeComponent();
        }

        public static void GetAssessmentNameData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Title from Assessment", con);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                AssessmentName.Add((string)reader["Title"]);
            }
            reader.Close();

        }

        private void btnStuAttendanceReport_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT sr.StudentId, s.FirstName+' '+s.LastName AS Name, s.RegistrationNumber,  SUM(CAST(CAST(rl.MeasurementLevel AS DECIMAL(10, 2)) / 4 * CAST((ac.TotalMarks)AS DECIMAL(10, 2)) AS DECIMAL(10, 2))) AS ObtainedMarks, a.TotalMarks as TotalAssessmentMarks, a.Title as AssessmentName FROM StudentResult AS sr INNER JOIN( SELECT Id, FirstName, LastName, RegistrationNumber FROM Student ) AS s ON sr.StudentId = s.Id INNER JOIN( SELECT MeasurementLevel, Id FROM RubricLevel ) AS rl ON rl.Id = sr.RubricMeasurementId AND s.Id = sr.StudentId INNER JOIN ( SELECT Id, TotalMarks, Name, RubricId, AssessmentId FROM AssessmentComponent ) AS ac ON ac.Id = sr.AssessmentComponentId INNER JOIN ( SELECT Id, cloId FROM Rubric) AS r ON r.Id = ac.RubricId INNER JOIN( SELECT Id, TotalMarks,Title FROM Assessment ) AS a ON a.Id = ac.AssessmentId where a.Title = @Title GROUP BY sr.StudentId, s.FirstName, s.LastName, s.RegistrationNumber, a.TotalMarks,a.Title", con);
            cmd.Parameters.AddWithValue("@Title", cboAssessmentName.Text);
            string name = cboAssessmentName.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvAssessment.DataSource = dt;

            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            document.SetMargins(36f, 36f, 36f, 36f);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("AssessmentReport.pdf", FileMode.Create));
            document.Open();
            Paragraph paragraph = new Paragraph("Assessment Report", FontFactory.GetFont("Times New Roman"));
            paragraph.Font.Size = 20;
            Paragraph paragraph1 = new Paragraph("Student Management System", FontFactory.GetFont("Times New Roman"));
            paragraph1.Font.Size = 22;
            Chunk para = new Chunk("            Assessment Name :" + " " + name, FontFactory.GetFont("Times New Roman"));
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

            PdfPTable table = new PdfPTable(gvAssessment.Columns.Count);
            table.WidthPercentage = 75;

            // Add header column to the table
            foreach (DataGridViewColumn column in gvAssessment.Columns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText));
                headerCell.BackgroundColor = BaseColor.GRAY; // Set the background color of the header cell
                table.AddCell(headerCell);
            }

            // Add data rows to the table
            foreach (DataGridViewRow row in gvAssessment.Rows)
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

        private void AssessmentReport_Load(object sender, EventArgs e)
        {
            GetAssessmentNameData();

            
            cboAssessmentName.DataSource = AssessmentName;

            List<object> uniqueItems = new List<object>();
            foreach (object item in cboAssessmentName.Items)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }

            cboAssessmentName.DataSource = new BindingSource(uniqueItems, null);
        }
    }
}
