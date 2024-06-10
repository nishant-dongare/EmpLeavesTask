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

namespace EmpLeavesTask
{
    public partial class HrPayslip : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
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

            txtLeavesTaken.Text = LeaveCalculations.GetRemainingLeaves(int.Parse(txtEmpNo.Text), firstDayOfLastMonth.Month, firstDayOfLastMonth.Year).ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("EXEC GetEmployeeById @EmployeeId", con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", txtEmpNo.Text);
                    con.Open();
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

                    }
                }
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            // Calculate the salary based on input values
            int monthlySalary = int.Parse(txtMonthlySalary.Text);
            int totalWorkingDays = int.Parse(txtTotalWorkingDays.Text);
            int leavesTaken = int.Parse(txtLeavesTaken.Text);

            int salaryPerDay = monthlySalary / totalWorkingDays;
            int calculatedSalary = monthlySalary - (salaryPerDay * leavesTaken);

            lblCalculatedSalary.Text = calculatedSalary.ToString();
        }

        /*protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
        }*/

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            // Convert Panel content to string
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            Panel1.RenderControl(htmlTextWriter);
            string panelContent = stringWriter.ToString();

            // Create PDF document
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath("~/PanelContent.pdf"), FileMode.Create));
            pdfDoc.Open();
            using (StringReader sr = new StringReader(panelContent))
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            }
            pdfDoc.Close();

            // Download the PDF file
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=PanelContent.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.WriteFile(Server.MapPath("~/PanelContent.pdf"));
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // To avoid the exception "Control 'Panel1' of type 'Panel' must be placed inside a form tag with runat=server."
        }

    }
}