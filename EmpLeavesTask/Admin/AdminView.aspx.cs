using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Ocsp;

namespace EmpLeavesTask.Admin
{
    public partial class AdminView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindClosedTickets();
            }
        }

        protected void BindClosedTickets()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"EXEC GetSolutionsByDateRange '{ddlViewBy.SelectedValue}'";

                /*switch (ddlViewBy.SelectedValue)
                {
                    case "Daily":
                        query = "EXEC GetSolutionsByDateRange 'Daily'";
                        break;
                    case "Weekly":
                        query = "EXEC GetSolutionsByDateRange 'Weekly'";
                        break;
                    case "Monthly":
                        query = "EXEC GetSolutionsByDateRange 'Monthly'";
                        break;
                }*/

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    GridViewClosedTickets.DataSource = dt;
                    GridViewClosedTickets.DataBind();
                }
            }
        }

        protected void ddlViewBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClosedTickets();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ClosedTickets.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            GridViewClosedTickets.AllowPaging = false;
            BindClosedTickets();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int k = 0; k < GridViewClosedTickets.Columns.Count; k++)
            {
                // Add header
                sb.Append(GridViewClosedTickets.Columns[k].HeaderText + ',');
            }
            sb.Append("\r\n");

            for (int i = 0; i < GridViewClosedTickets.Rows.Count; i++)
            {
                for (int k = 0; k < GridViewClosedTickets.Columns.Count; k++)
                {
                    // Add data
                    sb.Append(GridViewClosedTickets.Rows[i].Cells[k].Text.Replace(",", string.Empty) + ',');
                }
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();

            GridViewClosedTickets.AllowPaging = true;
            BindClosedTickets(); // Rebind data after exporting
        }
    }
}
