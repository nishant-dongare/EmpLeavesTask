﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask
{
    public partial class EmpLeaveApply : System.Web.UI.Page
    {
        int empId;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                empId = Convert.ToInt32(Request.QueryString["empId"]);
                int balanceLeaves = LeaveCalculations.GetBalanceLeaves(empId, DateTime.Today.Month, DateTime.Today.Year);
                balanceLeaves = balanceLeaves > 2 ? balanceLeaves - 2 : 2 - balanceLeaves;
                balanceLeaves = balanceLeaves < 0 ? 0 : balanceLeaves;
                Label5.Text = "Balance : " + balanceLeaves;
                totalleaves_lbl.Text = LeaveCalculations.TotalLeaves(empId, DateTime.Today.Month, DateTime.Today.Year).ToString();
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            // Parse the dates from the TextBoxes
            DateTime fromDate;
            DateTime toDate;
            empId = Convert.ToInt32(Session["empId"]);


            if (DateTime.TryParse(tb_fromdate.Text, out fromDate) && DateTime.TryParse(tb_todate.Text, out toDate))
            {
                if (fromDate > toDate)
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Invalid Dates</div>";
                    return;
                }
                else if (txtreason.Text == "")
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Invalid Reason</div>";
                }
                // Extract the month and year from the parsed dates
                int fromMonth = fromDate.Month;
                int fromYear = fromDate.Year;
                int toMonth = toDate.Month;
                int toYear = toDate.Year;

                int extraLeaves = 0;
                if (fromYear == toYear && fromMonth == toMonth)
                {

                    int WorkingDays = LeaveCalculations.CalculateWorkingDays(fromDate, toDate);
                    int remainingLeaves = LeaveCalculations.GetBalanceLeaves(empId, fromMonth, fromYear);
                    extraLeaves = WorkingDays > remainingLeaves ? WorkingDays - remainingLeaves : 0;

                    string query = $@"EXEC InsertLeaveApplication '{fromDate}','{toDate}','{txtreason.Text}','{fromDate.Month}','{fromDate.Year}','{WorkingDays}','{empId}','0'";

                    if (InsertIntoDB(query) > 0)
                    {
                        ltMessage.Text = "<div class='alert alert-success'>Leave Requested Successfully</div>";
                    }
                    else
                    {
                        ltMessage.Text = "<div class='alert alert-danger'>Task Failed Successfully.</div>";
                    }
                }
                else
                {
                    DateTime lastDateOfFromDate = new DateTime(fromYear, fromMonth, DateTime.DaysInMonth(fromYear, fromMonth));
                    DateTime newMonthDate = new DateTime(toYear, toMonth, 1);
                    int m1WorkingDays = LeaveCalculations.CalculateWorkingDays(fromDate, lastDateOfFromDate);
                    int m2WorkingDays = LeaveCalculations.CalculateWorkingDays(newMonthDate, toDate);
                    int m1 = m1WorkingDays - LeaveCalculations.GetBalanceLeaves(empId, fromMonth, fromYear);
                    int m2 = m2WorkingDays - LeaveCalculations.GetBalanceLeaves(empId, toMonth, toYear);

                    m1 = m1 < 0 ? 0 : m1;
                    m2 = m2 < 0 ? 0 : m2;

                    extraLeaves = m1 + m2;

                    int result = InsertIntoDB($"EXEC InsertLeaveApplication '{fromDate}','{lastDateOfFromDate}','{txtreason.Text}','{fromDate.Month}','{fromDate.Year}','{m1WorkingDays}','{empId}','0'");
                    if (result > 0)
                    {
                        InsertIntoDB($@"EXEC InsertLeaveApplication '{newMonthDate}','{toDate}','{txtreason.Text}','{toDate.Month}','{toDate.Year}','{m1WorkingDays}','{empId}','{result}'");
                        ltMessage.Text = "<div class='alert alert-success'>Leave Requested Successfully</div>";
                    }
                    else
                    {
                        ltMessage.Text = "<div class='alert alert-danger'>Task Failed Successfully.</div>";
                    }

                }

                double salaryDeduction = extraLeaves > 0 ? extraLeaves * (30000 / 30) : 0;
                //Response.Write(salaryDeduction);

            }
            else
            {
                Response.Write("Invalid date input.");
            }

        }

        private int InsertIntoDB(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    conn.Close();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return 0;
        }
    }
}