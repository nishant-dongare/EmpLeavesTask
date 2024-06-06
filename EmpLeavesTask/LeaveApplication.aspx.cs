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

                int firstMonthLeaves = 0, secondMonthLeaves = 0,absentLeaves=0;

                // You can now use fromMonth, fromYear, toMonth, and toYear as needed
                // For example, you might display them in a Label or use them in further processing
                //Response.Write($"From Date: {fromMonth}/{fromYear}<br/>To Date: {toMonth}/{toYear}");

                int allowedLeaves = 2;
                int usedLeaves;
                if (fromYear == toYear && fromMonth == toMonth) {
                    usedLeaves = GetUsedLeaves(empId, fromMonth, fromYear);
                }
                else
                {
                    firstMonthLeaves = GetUsedLeaves(empId, fromMonth, fromYear);
                    secondMonthLeaves = GetUsedLeaves(empId, toMonth, toYear);

                    int m1 = allowedLeaves > firstMonthLeaves ? allowedLeaves - firstMonthLeaves : 0;
                    int m2 = allowedLeaves > secondMonthLeaves ? allowedLeaves - secondMonthLeaves : 0;

                    m1 = GetRemainingDaysInMonth(fromDate)-allowedLeaves;
                    m2 = GetRemainingDaysInMonth(toDate) - allowedLeaves;
                    absentLeaves = m1 + m2;


                }
                int workingDays = CalculateWorkingDays(fromDate, toDate);
                int remailningLeaves = allowedLeaves > usedLeaves? allowedLeaves-usedLeaves: 0;


                int extraLeaves = workingDays - remailningLeaves;

                // Calculate salary deduction (example: $100 per extra leave)
                double salaryDeduction = extraLeaves > 0 ? extraLeaves * (30000 / 30) : 0;

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


        private int GetUsedLeaves(int empId, int month, int year)
        {
            int usedLeaves = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM EmployeeLeave WHERE emp_id = @EmpId AND MONTH(leave_date) = @Month AND YEAR(leave_date) = @Year";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmpId", empId);
                command.Parameters.AddWithValue("@Month", month);
                command.Parameters.AddWithValue("@Year", year);
                connection.Open();
                usedLeaves = (int)command.ExecuteScalar();
            }
            return usedLeaves;
        }

        private int GetRemainingDaysInMonth(DateTime date)
        {
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            DateTime lastDayOfMonth = new DateTime(date.Year, date.Month, daysInMonth);
            int remainingDays = (lastDayOfMonth - date).Days;
            return remainingDays;
        }


    }
}