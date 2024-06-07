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
    public partial class LeaveApplication : System.Web.UI.Page
    {
        int empId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                empId = int.Parse(Request.QueryString["id"]);
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            // Parse the dates from the TextBoxes
            DateTime fromDate;
            DateTime toDate;

            if (DateTime.TryParse(txtfromdate.Text, out fromDate) && DateTime.TryParse(txttodate.Text, out toDate))
            {
                // Extract the month and year from the parsed dates
                int fromMonth = fromDate.Month;
                int fromYear = fromDate.Year;
                int toMonth = toDate.Month;
                int toYear = toDate.Year;

                // You can now use fromMonth, fromYear, toMonth, and toYear as needed
                // For example, you might display them in a Label or use them in further processing
                //Response.Write($"From Date: {fromMonth}/{fromYear}<br/>To Date: {toMonth}/{toYear}");

                int allowedLeaves = 2;
                int extraLeaves=0;
                if (fromYear == toYear && fromMonth == toMonth) {
                    int WorkingDays = CalculateWorkingDays(fromDate, toDate);
                    int remainingLeaves = GetRemainingLeaves(empId, fromMonth, fromYear);
                    extraLeaves = WorkingDays > remainingLeaves ? WorkingDays - remainingLeaves : 0;
                }
                else
                {
                    int m1 = CalculateWorkingDays(fromDate,fromDate) - GetRemainingLeaves(empId, fromMonth, fromYear);
                    int m2 = CalculateWorkingDays(new DateTime(toYear, toMonth, 1), toDate) - GetRemainingLeaves(empId, toMonth, toYear);

                    m1 = m1 < 0 ? 0 : m1;
                    m2 = m2 < 0 ? 0 : m2;

                    extraLeaves = m1 + m2;
                }

                double salaryDeduction = extraLeaves > 0 ? extraLeaves * (30000 / 30) : 0;
                Response.Write(salaryDeduction);

            }
            else
            {
                // Handle invalid date input
                Response.Write("Invalid date input.");
            }

        }

        private int CalculateWorkingDays(DateTime from, DateTime to)
        {
            // Ensure from is always less than or equal to to
            if (from > to)
            {
                DateTime temp = from;
                from = to;
                to = temp;
            }

            int totalDays = (int)(to - from).TotalDays + 1;
            int workingDays = 0;

            for (int i = 0; i < totalDays; i++)
            {
                DateTime currentDay = from.AddDays(i);
                if (currentDay.DayOfWeek != DayOfWeek.Saturday && currentDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
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
            return usedLeaves>2?0:2-usedLeaves;
        }

        private int GetRemainingDaysInMonth(DateTime date)
        {
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            DateTime lastDayOfMonth = new DateTime(date.Year, date.Month, daysInMonth);
            int remainingDays = (lastDayOfMonth - date).Days;
            return remainingDays+1;
        }
    }
}