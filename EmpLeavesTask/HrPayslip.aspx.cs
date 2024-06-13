using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using Org.BouncyCastle.Utilities.Collections;
using System.Drawing;
using Org.BouncyCastle.Ocsp;
using System.Reflection.Emit;

namespace EmpLeavesTask
{
    public partial class HrPayslip : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(connectionString);
            conn.Open();
        }

        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            // Fetch and display employee details based on entered Emp No.
            // Example data fetching:

            txtBankName.Text = "ABC Bank";
            txtContactNo.Text = "1234567890";
            txtBankAccountNo.Text = "1234567890123456";
            txtEmail.Text = "john.doe@example.com";
            txtDesignation.Text = "Software Engineer";
            txtDOJ.Text = "01/01/2020";
            txtMonthlySalary.Text = "30000";
            DateTime today = DateTime.Today;
            DateTime firstDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            DateTime lastDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);

            txtTotalWorkingDays.Text = LeaveCalculations.CalculateWorkingDays(firstDayOfLastMonth, lastDayOfLastMonth).ToString();


            using (SqlCommand cmd = new SqlCommand("EXEC GetEmployeeById @EmployeeId", conn))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", txtEmpNo.Text);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Read();
                    txtEmpName.Text = rdr["ename"].ToString();
                    txtContactNo.Text = rdr["contact"].ToString();
                    txtEmail.Text = rdr["email"].ToString();
                    txtDOJ.Text = rdr["doj"].ToString();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showalert", "alert('No Offer letters')", true);
                }
                rdr.Close();
            }
            int totalLeaves = LeaveCalculations.TotalLeaves(Convert.ToInt32(txtEmpNo.Text), lastDayOfLastMonth.Month, lastDayOfLastMonth.Year);
            txtBalanceLeaves.Text = (totalLeaves > 2 ? 0 : 2 - totalLeaves).ToString();
            txtLeavesTaken.Text = totalLeaves.ToString();
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            // Calculate the salary based on input values
            int monthlySalary = 30000;
            int leavesTaken = int.Parse(txtLeavesTaken.Text);
            leavesTaken = leavesTaken > 2 ? leavesTaken - 2 : 0;
            int salaryPerDay = monthlySalary / 30;
            int calculatedSalary = monthlySalary - (salaryPerDay * leavesTaken);

            lblCalculatedSalary.Text = calculatedSalary.ToString();
        }

        protected void Download_Invoice(object sender, EventArgs e)
        {
            string fileName = $"Payslip_{txtEmail.Text}.pdf";
            GeneratePdf1(fileName);
            //Save into database
            SavePayslip($@"Payslips/{fileName}");

            string directoryPath = Server.MapPath("~/PaySlips/");
            string filepath1 = Path.Combine(directoryPath,fileName);
            if (EmailManager.SendEmailWithPDF(txtEmail.Text, filepath1))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "EmailSuccess", "alert('Offer letter emailed successfully');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "EmailError", "alert('Error sending email');", true);
            }


            // For example, display a message to the user:
            //Response.Write("<script>alert('Payslip Generated Successfully!');</script>");

        }


        private void SavePayslip(string filePath)
        {
            DateTime lastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);

            string query = "EXEC InsertOrUpdatePayslip @Month, @Year, @Empid, @FilePath";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@Month", lastMonth.Month);
                command.Parameters.AddWithValue("@Year", lastMonth.Year);
                command.Parameters.AddWithValue("@Empid", txtEmpNo.Text);
                command.Parameters.AddWithValue("@FilePath", filePath);

                command.ExecuteNonQuery();
            }

        }


        protected string GeneratePdf1(string fileName)
        {
            int monthlySalary = 30000;
            int leavesTaken = int.Parse(txtLeavesTaken.Text);
            leavesTaken = leavesTaken > 2 ? leavesTaken - 2 : 0;
            int salaryPerDay = monthlySalary / 30;
            int calculatedSalary = monthlySalary - (salaryPerDay * leavesTaken);

            //string filePath = Server.MapPath($"~/Payslips/{Label2.Text}.pdf");
            //string monthName = DateTime.Now.ToString("MMMM");
            
            string filePath = Server.MapPath($"~/Payslips/{fileName}");
            // Ensure the directory exists
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))

            {

                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 10f, 0f);
                string email = txtEmail.Text; // User's email address fetched from the database
                                            //byte[] pdfBytes;

                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);
                    pdfDoc.Open();

                    //// Add Invoice header
                    //Paragraph header = new Paragraph("Masstech\nBusiness Solutions\nPayslip for the Month of 2023", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                    //header.Alignment = Element.ALIGN_CENTER;
                    ////pdfDoc.Add(header);
                    pdfDoc.Add(new iTextSharp.text.Paragraph("\n"));
                    pdfDoc.Add(new iTextSharp.text.Paragraph("\n"));

                    //string imagePath = Server.MapPath("~/Images/Masstech-Logo-Resize.png");
                    //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                    //logo.ScaleToFit(50f, 50f);

                    //string imagePath = Server.MapPath("~/Images/Masstech-Logo-Resize.png"); // Use the correct virtual path

                    //byte[] imageBytes = File.ReadAllBytes(imagePath);
                    //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imageBytes);
                    //logo.ScaleToFit(50f, 50f);
                    // Create a Chunk with the header text
                    Chunk headerChunk = new Chunk("Masstech Business Solutions Payslip ");

                    // Set the font of the Chunk
                    headerChunk.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

                    Paragraph headerParagraph = new Paragraph(headerChunk);

                    // Set alignment of the Paragraph to center
                    headerParagraph.Alignment = Element.ALIGN_CENTER;

                    PdfPCell headerCell = new PdfPCell(new Phrase(headerChunk));
                    headerCell.BackgroundColor = new BaseColor(169, 169, 169); // Set the background color here

                    // Set alignment of the PdfPCell content
                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //headerParagraph.BackgroundColor = new BaseColor(0, 0, 128); // Navy blue background

                    PdfPTable headerTable = new PdfPTable(1);
                    headerTable.WidthPercentage = 100;
                    headerTable.AddCell(headerCell);

                    // Add the Chunk to the PDF document
                    pdfDoc.Add(headerChunk);


                    // Add some spacing
                    pdfDoc.Add(new iTextSharp.text.Paragraph("\n"));

                    // Add Employee Details table
                    PdfPTable empTable = new PdfPTable(4);
                    empTable.WidthPercentage = 100;

                    BaseColor cellBackgroundColor = BaseColor.WHITE;//(169, 169, 169); // Light lavender background

                    // Create a method to add a cell with background color
                    void AddCellWithBackground(PdfPTable table, string text, BaseColor backgroundColor)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(text));
                        cell.BackgroundColor = backgroundColor;
                        table.AddCell(cell);
                    }

                    AddCellWithBackground(empTable, "NAME OF EMPLOYEE:", cellBackgroundColor);
                    AddCellWithBackground(empTable, txtEmpName.Text, BaseColor.WHITE);

                    AddCellWithBackground(empTable, "DESIGNATION:", cellBackgroundColor);
                    AddCellWithBackground(empTable, "Software Developer", BaseColor.WHITE);

                    AddCellWithBackground(empTable, "BANK NAME:", cellBackgroundColor);
                    AddCellWithBackground(empTable, "ICICI Bank", BaseColor.WHITE);

                    AddCellWithBackground(empTable, "EMPLOYEE EMAIL:", cellBackgroundColor);
                    AddCellWithBackground(empTable, txtEmail.Text, BaseColor.WHITE);

                    AddCellWithBackground(empTable, "IFSC CODE:", cellBackgroundColor);
                    AddCellWithBackground(empTable, "ICIC0000092", BaseColor.WHITE);

                    AddCellWithBackground(empTable, "DATE OF JOINING:", cellBackgroundColor);
                    AddCellWithBackground(empTable, txtDOJ.Text, BaseColor.WHITE);

                    AddCellWithBackground(empTable, "BANK ACCOUNT NO:", cellBackgroundColor);
                    AddCellWithBackground(empTable, "123801537392", BaseColor.WHITE);

                    AddCellWithBackground(empTable, "CONTACT NO:", cellBackgroundColor);
                    AddCellWithBackground(empTable, txtContactNo.Text, BaseColor.WHITE);

                    AddCellWithBackground(empTable, "PAN:", cellBackgroundColor);
                    AddCellWithBackground(empTable, "FGKPB0088L", BaseColor.WHITE);

                    AddCellWithBackground(empTable, "DAYS IN MONTH:", cellBackgroundColor);
                    AddCellWithBackground(empTable, "28", BaseColor.WHITE);

                    AddCellWithBackground(empTable, "AADHAR:", cellBackgroundColor);
                    AddCellWithBackground(empTable, "7902 8178 5003", BaseColor.WHITE);

                    AddCellWithBackground(empTable, "UAN:", cellBackgroundColor);
                    AddCellWithBackground(empTable, "NA", BaseColor.WHITE);


                    pdfDoc.Add(empTable);

                    // Add some spacing
                    pdfDoc.Add(new iTextSharp.text.Paragraph("\n"));

                    // Add Salary Details table
                    PdfPTable salaryTable = new PdfPTable(4);
                    salaryTable.WidthPercentage = 100;

                    AddCellWithBackground(salaryTable, "GROSS SALARY", cellBackgroundColor);
                    AddCellWithBackground(salaryTable, "AMOUNT", cellBackgroundColor);
                    AddCellWithBackground(salaryTable, "DEDUCTION", cellBackgroundColor);
                    AddCellWithBackground(salaryTable, "AMOUNT", cellBackgroundColor);

                    salaryTable.AddCell("Basic");
                    salaryTable.AddCell(monthlySalary.ToString());
                    salaryTable.AddCell("PF");
                    salaryTable.AddCell("-");

                    salaryTable.AddCell("HRA");
                    salaryTable.AddCell("0");
                    salaryTable.AddCell("Professional Tax");
                    salaryTable.AddCell("0");

                    salaryTable.AddCell("Travel Allowance");
                    salaryTable.AddCell("0");
                    salaryTable.AddCell("TDS");
                    salaryTable.AddCell("-");

                    salaryTable.AddCell("Bonus");
                    salaryTable.AddCell("0");
                    salaryTable.AddCell("");
                    salaryTable.AddCell("");

                    salaryTable.AddCell("Special Allowance");
                    salaryTable.AddCell("0");
                    salaryTable.AddCell("");
                    salaryTable.AddCell("");

                    salaryTable.AddCell("Medical Re-imbursement");
                    salaryTable.AddCell("0");
                    salaryTable.AddCell("");
                    salaryTable.AddCell("");

                    salaryTable.AddCell("GROSS SALARY");
                    salaryTable.AddCell(calculatedSalary.ToString());
                    salaryTable.AddCell("TOTAL DEDUCTION");
                    salaryTable.AddCell("0");

                    salaryTable.AddCell("NET SALARY PAID");
                    salaryTable.AddCell(calculatedSalary.ToString());
                    salaryTable.AddCell("");
                    salaryTable.AddCell("");

                    pdfDoc.Add(salaryTable);

                    // Add Footer
                    // Add Footer
                    // Add Footer
                    Chunk footerChunk = new Chunk("This is computerised generated salary slip and does not require authentication", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.ITALIC));
                    footerChunk.SetAnchor("http://www.example.com");
                    pdfDoc.Add(footerChunk);


                    pdfDoc.Close();

                }

                return filePath;
            }
        }

        public override void VerifyRenderingInServerForm(Control control) { }

        /*private byte[] GeneratePdfFromHtml(string htmlContent)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                using (StringReader stringReader = new StringReader(htmlContent))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);
                }

                document.Close();
                return memoryStream.ToArray();
            }
        }
        
        protected void Download_Invoice(object sender, EventArgs e)
        {
            // Path to save the PDF on the server
            /*string folderPath = Server.MapPath("~/Payslips/");
            string fileName = $@"Payslip_{txtEmail.Text}.pdf"; // Unique file name
            string filePath = Path.Combine(folderPath, fileName);

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Create the PDF document and write to the specified file path
            StringWriter sw = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(sw);
            Panel1.RenderControl(writer);
            StringReader stringReader = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 101, 10f, 100f, 0f);
            HTMLWorker worker = new HTMLWorker(pdfDoc);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                PdfWriter.GetInstance(pdfDoc, fs);
                pdfDoc.Open();
                worker.Parse(stringReader);
                pdfDoc.Close();
            }
            //Save into database
            SavePayslip($@"~/Payslips/{fileName}");


            // For example, display a message to the user:
            Response.Write("<script>alert('Payslip Generated Successfully!');</script>");
        }*/
    }
}