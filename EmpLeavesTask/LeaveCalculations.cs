using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmpLeavesTask
{
    public class LeaveCalculations
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

        public static int CalculateWorkingDays(DateTime from, DateTime to)
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


        public static int GetRemainingLeaves(int empId, int month, int year)
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