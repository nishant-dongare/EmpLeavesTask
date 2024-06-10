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

            txtEmpName.Text = "John Doe";
            txtBankName.Text = "ABC Bank";
            txtContactNo.Text = "1234567890";
            txtBankAccountNo.Text = "1234567890123456";
            txtEmail.Text = "john.doe@example.com";
            txtDesignation.Text = "Software Engineer";
            txtDOJ.Text = "01/01/2020";
            txtMonthlySalary.Text = "30000";

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

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
        }

    }
}