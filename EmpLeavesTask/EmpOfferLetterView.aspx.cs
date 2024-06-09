using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask
{
    public partial class EmpOfferLetterView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //pnlOfferLetter.Visible = false;
            LoadOfferLetter();

        }
        private void LoadOfferLetter()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            string query = "SELECT TOP 1 * FROM OfferLetters WHERE email = @Email"; // Modify this query as needed

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Assuming you have the email to fetch the specific offer letter, replace this with the appropriate parameter
                    command.Parameters.AddWithValue("@Email", Session["uid"].ToString());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string name = reader["name"].ToString();
                        string email = reader["email"].ToString();
                        string doj = reader["doj"].ToString();
                        string filepath = reader["filepath"].ToString();

                        string cardHtml = $@"
                        <div class='card' style='width: 100%;'>
                            <div class='row no-gutters'>
                                    <div class='card-body'>
                                        <h5 class='card-title'>{name}</h5>
                                        <p class='card-text'><strong>Email:</strong> {email}</p>
                                        <p class='card-text'><strong>Date of Joining:</strong> {doj}</p>
                                        <a href='{ResolveUrl(filepath)}' class='btn btn-primary' target='_blank'>View Offer Letter</a>
                                        <a href='{ResolveUrl(filepath)}' class='btn btn-primary' download>Download Letter</a>
                                    </div>
                            </div>
                        </div>";

                        pnlOfferLetter.Visible = true;
                        pnlOfferLetter.Controls.Add(new LiteralControl(cardHtml));
                        //pnlOfferLetter.Controls.Add(new LiteralControl($"<embed type='application/pdf' width='100%' height='600px' src='{filepath}'/>"));
                    }
                    else
                    {
                        pnlOfferLetter.Controls.Add(new LiteralControl("<p>No offer letter found for this employee.</p>"));
                    }

                    reader.Close();
                }
            }
        }

        protected void btnViewOfferLetter_Click(object sender, EventArgs e)
        {

            LoadOfferLetter();

        }
    }
}