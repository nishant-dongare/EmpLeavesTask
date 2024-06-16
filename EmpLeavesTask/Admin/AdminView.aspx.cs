using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask.Admin
{
    public partial class AdminView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            DateTime fromDate, toDate;
            if (DateTime.TryParse(tb_fromdate.Text, out fromDate) && DateTime.TryParse(tb_todate.Text, out toDate))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
                string query = "SELECT * FROM Solution WHERE ticket_date BETWEEN @FromDate AND @ToDate";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
            else
            {
                ltMessage.Text = "Please enter valid dates.";
            }
        }
    }
}