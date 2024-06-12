using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Org.BouncyCastle.Ocsp;

namespace EmpLeavesTask
{
    public class LeaveCalculations
    {
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

        public static int TotalLeaves(int empId, int month, int year) {
            int total = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "EXEC GetCountOfLeaves @EmpId,@Month,@Year";

                //string query = "SELECT COUNT(*) FROM LeaveApplication WHERE emp_id = @EmpId AND MONTH(fromdate) = @Month AND YEAR(fromdate) = @Year";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@EmpId", empId);
                command.Parameters.AddWithValue("@Month", month);
                command.Parameters.AddWithValue("@Year", year);
                conn.Open();
                object result = command.ExecuteScalar();
                conn.Close();
                if (result != null && result != DBNull.Value)
                {
                    total = (int)result;
                }
                else
                {
                    total = 0; // or handle the null case appropriately
                }
            }
            return total;
        }

        public static int GetBalanceLeaves(int empId, int month, int year)
        {
            int balanceLeaves = TotalLeaves( empId,month, year);
            return balanceLeaves > 2 ? 0 : 2 - balanceLeaves;
        }
    }
}