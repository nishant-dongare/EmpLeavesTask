using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask.Ticket
{
    public partial class ViewTickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DownloadAttachmentRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                string filepath = drv["attachment"].ToString();
                string ticketId = drv["ticket_id"].ToString(); 
                Literal litDownload = (Literal)e.Row.FindControl("litDownload");
                Literal litCloseSolution = (Literal)e.Row.FindControl("litCloseSolution");
                if (litDownload != null)
                {
                    litDownload.Text = $"<a href ='{ResolveUrl(filepath)}' class='btn btn-dark' download>Download</a>";
                    litCloseSolution.Text = $"<a href ='{ResolveUrl(ticketId)}' class='btn btn-dark'>Close Solution</a>";
                }
            }
        }
    }
}