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