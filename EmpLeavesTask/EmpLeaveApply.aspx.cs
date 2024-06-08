using System;
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

                // You can now use fromMonth, fromYear, toMonth, and toYear as needed
                // For example, you might display them in a Label or use them in further processing
                //Response.Write($"From Date: {fromMonth}/{fromYear}<br/>To Date: {toMonth}/{toYear}");

                int allowedLeaves = 2;
                int extraLeaves = 0;
                if (fromYear == toYear && fromMonth == toMonth)
                {

                    int WorkingDays = CalculateWorkingDays(fromDate, toDate);
                    int remainingLeaves = GetRemainingLeaves(empId, fromMonth, fromYear);
                    extraLeaves = WorkingDays > remainingLeaves ? WorkingDays - remainingLeaves : 0;

                    string query = $@"EXEC InsertLeaveApplication '{fromDate}','{toDate}','{txtreason.Text}','{fromDate.Month}','{fromDate.Year}','{WorkingDays}','{empId}','0'";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                ltMessage.Text = "<div class='alert alert-success'>Employee added successfully!</div>";
                            }
                            else
                            {
                                ltMessage.Text = "<div class='alert alert-danger'>Task Failed Successfully.</div>";
                            }
                        }
                    }
                }
                else
                {
                    DateTime lastDateOfFromDate = new DateTime(fromYear, fromMonth, DateTime.DaysInMonth(fromYear, fromMonth));
                    DateTime newMonthDate = new DateTime(toYear, toMonth, 1);
                    int m1WorkingDays = CalculateWorkingDays(fromDate, lastDateOfFromDate);
                    int m2WorkingDays = CalculateWorkingDays(newMonthDate, toDate);
                    int m1 = m1WorkingDays - GetRemainingLeaves(empId, fromMonth, fromYear);
                    int m2 = m2WorkingDays - GetRemainingLeaves(empId, toMonth, toYear);

                    m1 = m1 < 0 ? 0 : m1;
                    m2 = m2 < 0 ? 0 : m2;

                    extraLeaves = m1 + m2;

                    int result = InsertIntoDB($@"EXEC InsertLeaveApplication '{fromDate}','{lastDateOfFromDate}','{txtreason.Text}','{fromDate.Month}','{fromDate.Year}','{m1WorkingDays}','{empId}','0'");
                    if (result > 0)
                    {
                        InsertIntoDB($@"EXEC InsertLeaveApplication '{newMonthDate}','{toDate}','{txtreason.Text}','{fromDate.Month}','{fromDate.Year}','{m1WorkingDays}','{empId}','{result}'");
                        ltMessage.Text = "<div class='alert alert-success'>Employee added successfully!</div>";

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
                // Handle invalid date input
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
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return 0;
        }

        private int CalculateWorkingDays(DateTime from, DateTime to)
        {
            // Ensure from is always less than or equal to to
            /* if (from > to)
             {
                 DateTime temp = from;
                 from = to;
                 to = temp;
             }
             int totalDays = (int)(to - from).TotalDays + 1;
             */

            int workingDays = 0;
            DateTime currentDay = from;
            while (currentDay <= to)
            {
                if (currentDay.DayOfWeek != DayOfWeek.Saturday && currentDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
                currentDay = currentDay.AddDays(1);
            }
            return workingDays;
        }


        private int GetRemainingLeaves(int empId, int month, int year)
        {
            int usedLeaves = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT countofleaves FROM LeaveApplication WHERE emp_id = @EmpId";

                //string query = "SELECT COUNT(*) FROM LeaveApplication WHERE emp_id = @EmpId AND MONTH(fromdate) = @Month AND YEAR(fromdate) = @Year";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmpId", empId);
                //command.Parameters.AddWithValue("@Month", month);
                //command.Parameters.AddWithValue("@Year", year);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    usedLeaves = (int)result;
                }
                else
                {
                    usedLeaves = 0; // or handle the null case appropriately
                }
            }
            return usedLeaves > 2 ? 0 : 2 - usedLeaves;
        }
    }

}