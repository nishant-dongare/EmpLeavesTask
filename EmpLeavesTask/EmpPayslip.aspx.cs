using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask
{
    public partial class EmpPayslip : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*protected void btnViewPayslip_Click(object sender, EventArgs e)
        {
            string month = txtMonth.Text.Trim();
            string year = txtYear.Text.Trim();
            int empId;
            if (!int.TryParse(txtEmpID.Text.Trim(), out empId))
            {
                litMessage.Text = "Invalid Employee ID";
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetPayslipFilepath", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@month", month);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@emp_id", empId);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != null)
                    {
                        string filepath = result.ToString();
                        litMessage.Text = $"<a href='{filepath}' target='_blank'>View Payslip</a>";
                    }
                    else
                    {
                        litMessage.Text = "Payslip not found.";
                    }
                }
            }
        }*/

        protected void gvPayslips_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                string filepath = drv["filepath"].ToString();
                Literal litMessage = (Literal)e.Row.FindControl("litMessage");
                Literal litDownload = (Literal)e.Row.FindControl("litDownload");
                if (litMessage != null)
                {
                    litMessage.Text = $"<a href='{ResolveUrl(filepath)}' target='_blank'>View Payslip</a>";
                    litDownload.Text = $"<a href ='{ResolveUrl(filepath)}' class='btn btn-primary' download>Download</a>";

                }
}
        }
    }
}