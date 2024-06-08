using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask
{
    public partial class HrLeaveApprove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            string connectionString = WebConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateLeaveStatus", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}