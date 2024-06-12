using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask
{
    public partial class HrLeaveApprove : System.Web.UI.Page
    {
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            conn = new SqlConnection(connectionString);
            conn.Open();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string newStatus = e.CommandName == "Approve" ? "Approved" : "Rejected";

                UpdateLeaveStatus(index, newStatus);

                // Rebind the GridView to reflect the changes
                GridView2.DataBind();
            }
        }

        private void UpdateLeaveStatus(int requestId, string newStatus)
        {
            using (SqlCommand cmd = new SqlCommand("UpdateLeaveStatus", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestId", requestId);
                cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                cmd.ExecuteNonQuery();
            }
        }

        /*public int LeaveCountIncrement(int empId) {
            using (SqlCommand cmd = new SqlCommand($@"SELECT countofleaves from LeaveApplication WHERE emp_id = @EmployeeId", con))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", empId);
                //con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    txtLeavesTaken.Text = result.ToString();
                    txtBalanceLeaves.Text = (2 - Convert.ToInt32(result)).ToString();
                    if (Convert.ToInt32(txtBalanceLeaves.Text) < 0)
                    {
                        txtBalanceLeaves.Text = "0";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Leave count is not fetching!');</script>");
                }
            }
        }*/

        public int FetchLeaveCount(int empId)
        {
            using (SqlCommand cmd = new SqlCommand("EXEC GetCountOfLeaves @EmployeeId", conn))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", empId);
                //con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    //Response.Write("<script>alert('Leave count is not fetching!');</script>");
                    return 0;
                }
            }
        }
    }
}