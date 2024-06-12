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
                    Response.Write("<script>alert('Employee data is not fetching!');</script>");
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
            // Path to save the PDF on the server
            string folderPath = Server.MapPath("~/Payslips/");
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
        }


        private byte[] GeneratePdfFromHtml(string htmlContent)
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

        private void SavePayslip(string filePath)
        {
            DateTime lastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);

            string query = "INSERT INTO Payslips(month, year, emp_id, filepath) VALUES(@Month, @Year, @Empid, @Filepath)";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@Month", lastMonth.Month);
                command.Parameters.AddWithValue("@Year", lastMonth.Year);
                command.Parameters.AddWithValue("@Empid", txtEmpNo.Text);
                command.Parameters.AddWithValue("@FilePath", filePath);

                command.ExecuteNonQuery();
            }

        }

        public override void VerifyRenderingInServerForm(Control control) { }
    }
}