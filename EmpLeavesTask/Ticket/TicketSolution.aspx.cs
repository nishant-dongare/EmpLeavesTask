using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask.Ticket
{
    public partial class TicketSolution : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Assuming the ticket ID is passed as a query parameter
                string ticketId = Request.QueryString["ticket_id"];
                if (!string.IsNullOrEmpty(ticketId))
                {
                    LoadTicketDetails(int.Parse(ticketId));
                }
            }
        }

        private void LoadTicketDetails(int ticketId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ticket_id, ticket FROM RaiseTicket WHERE ticket_id = @ticket_id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ticket_id", ticketId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ticketIdTb.Text = reader["ticket_id"].ToString();
                        ticketTb.Text = reader["ticket"].ToString();
                    }
                    con.Close();
                }
            }
        }

        protected void SubmitSolution_Click(object sender, EventArgs e)
        {
            int ticketId = int.Parse(ticketIdTb.Text);
            string ticketSolution = solutionTb.Text;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AddTicketSolution", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ticket_id", ticketId);
                    cmd.Parameters.AddWithValue("@ticket_solution", ticketSolution);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            // Redirect to a confirmation page or show a success message
            Response.Redirect("SolutionConfirmation.aspx");
        }
    }
}