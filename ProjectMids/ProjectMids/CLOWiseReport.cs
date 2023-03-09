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
    public partial class CLOWiseReport : Form
    {
        public static List<string> cloName = new List<string>();
        public CLOWiseReport()
        {
            InitializeComponent();
        }

        public static void GetCloNameData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Name from Clo", con);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                cloName.Add((string)reader["Name"]);
            }
            reader.Close();

        }

        private void btnCLOReport_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select sr.StudentId,s.FirstName+' '+s.LastName as Name,s.RegistrationNumber,ac.Name as Component,r.CloId, cast(cast(rl.MeasurementLevel as decimal(10, 2)) / 4 * cast((ac.TotalMarks) as decimal(10, 2)) as decimal(10, 2)) as ObtainedMarks, ac.TotalMarks, rl.MeasurementLevel from StudentResult as sr INNER JOIN( Select Id, FirstName, LastName, RegistrationNumber from Student) as s ON sr.StudentId = s.Id  INNER JOIN(Select MeasurementLevel, Id from RubricLevel) as rl ON rl.Id = sr.RubricMeasurementId and s.Id = sr.StudentId  INNER JOIN(Select Id, TotalMarks, Name, RubricId from AssessmentComponent ) as ac ON ac.Id = sr.AssessmentComponentId INNER JOIN(Select Id, cloId from Rubric) as r ON r.Id = ac.RubricId INNER JOIN (Select Name, Id from Clo) as c ON c.Id = r.CloId where c.Name = @Name", con);
            cmd.Parameters.AddWithValue("@Name", cboCLOId.Text);
            string name = cboCLOId.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvCLOWise.DataSource = dt;

            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            document.SetMargins(36f, 36f, 36f, 36f);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("CLOWiseReport.pdf", FileMode.Create));

            document.Open();
            Paragraph paragraph = new Paragraph("CLO Report", FontFactory.GetFont("Times New Roman"));
            paragraph.Font.Size = 20;
            Paragraph paragraph1 = new Paragraph("Student Management System", FontFactory.GetFont("Times New Roman"));
            paragraph1.Font.Size = 22;
            Chunk para = new Chunk("            CLO Name :" + name, FontFactory.GetFont("Times New Roman"));
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

            PdfPTable table = new PdfPTable(gvCLOWise.Columns.Count);
            table.WidthPercentage = 100;

            // Add header column to the table
            foreach (DataGridViewColumn column in gvCLOWise.Columns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText));
                headerCell.BackgroundColor = BaseColor.GRAY; // Set the background color of the header cell
                table.AddCell(headerCell);
            }
            // Add data rows to the table
            foreach (DataGridViewRow row in gvCLOWise.Rows)
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

        private void CLOWiseReport_Load(object sender, EventArgs e)
        {
            GetCloNameData();

            //cboCLOId.Text = "Select CLO Id";
            cboCLOId.DataSource = cloName;

            List<object> uniqueItems = new List<object>();
            foreach (object item in cboCLOId.Items)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }

            cboCLOId.DataSource = new BindingSource(uniqueItems, null);
        }

        private void btnAllCLO_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select sr.StudentId,s.FirstName+' '+s.LastName as Name,s.RegistrationNumber,ac.Name as Component,r.CloId, cast(cast(rl.MeasurementLevel as decimal(10, 2)) / 4 * cast((ac.TotalMarks) as decimal(10, 2)) as decimal(10, 2)) as ObtainedMarks, ac.TotalMarks, rl.MeasurementLevel from StudentResult as sr INNER JOIN( Select Id, FirstName, LastName, RegistrationNumber from Student) as s ON sr.StudentId = s.Id  INNER JOIN(Select MeasurementLevel, Id from RubricLevel) as rl ON rl.Id = sr.RubricMeasurementId and s.Id = sr.StudentId  INNER JOIN(Select Id, TotalMarks, Name, RubricId from AssessmentComponent ) as ac ON ac.Id = sr.AssessmentComponentId INNER JOIN(Select Id, cloId from Rubric) as r ON r.Id = ac.RubricId ", con);           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvCLOWise.DataSource = dt;

            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            document.SetMargins(36f, 36f, 36f, 36f);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("ALLCLOReport.pdf", FileMode.Create));

            document.Open();
            Paragraph paragraph = new Paragraph("CLO Report", FontFactory.GetFont("Times New Roman"));
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
 

            PdfPTable table = new PdfPTable(gvCLOWise.Columns.Count);
            table.WidthPercentage = 100;

            // Add header column to the table
            foreach (DataGridViewColumn column in gvCLOWise.Columns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText));
                headerCell.BackgroundColor = BaseColor.GRAY; // Set the background color of the header cell
                table.AddCell(headerCell);
            }
            // Add data rows to the table
            foreach (DataGridViewRow row in gvCLOWise.Rows)
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

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
