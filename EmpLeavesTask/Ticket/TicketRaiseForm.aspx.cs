using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask.Ticket
{
    public partial class TicketRaiseForm : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["email"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                // Sample code to populate email address
                lblEmail.Text = Session["email"].ToString();

                //ddlRole.Items.Add(new ListItem("-- Select Role --", ""));
                ddlRole.Items.Add(new ListItem("HR", "HR"));
                ddlRole.Items.Add(new ListItem("Trainer", "Trainer"));
                ddlRole.Items.Add(new ListItem("Trainee", "Trainee"));

            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedValue != "")
            {
                LoadEmployeesByRole(ddlRole.SelectedValue);
                ddlRaisedTo.Enabled = true;
            }
            else
            {
                ddlRaisedTo.Items.Clear();
                ddlRaisedTo.Items.Insert(0, new ListItem("-- Select --", ""));
                ddlRaisedTo.Enabled = false;
            }
        }

        private void LoadEmployeesByRole(string role)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "EXEC GetEmployeesByRole @role";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@role", role);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                ddlRaisedTo.DataSource = rdr;
                ddlRaisedTo.DataTextField = "ename";
                ddlRaisedTo.DataValueField = "emp_id";
                ddlRaisedTo.DataBind();
            }
            ddlRaisedTo.Items.Insert(0, new ListItem("-- Select --", ""));
        }

       protected void btnRaiseTicket_Click(object sender, EventArgs e)
        {
            int raisedTo = Convert.ToInt32(ddlRaisedTo.SelectedValue); // Assumes you have a TextBox with ID txtRaisedTo
            int raisedBy = Convert.ToInt32(Session["emp_id"]); // Assumes you have a TextBox with ID txtRaisedBy
            string ticket = ticket_tb.Text; // Assumes you have a TextBox with ID txtTicket
            string attachment = ""; // Assumes you have a TextBox with ID txtAttachment

            if (fileupload1.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fileupload1.PostedFile.FileName);
                    string folderPath = Server.MapPath("~/Attachments/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    fileupload1.SaveAs(Path.Combine(folderPath, filename));
                    attachment = "~/Attachments/" + filename;
                }
                catch (Exception ex)
                {
                    ltMessage.Text = "File upload failed: " + ex.Message;
                    return;
                }
            }

            CreateRaiseTicket(raisedTo, raisedBy, ticket, attachment);
        }

        public void CreateRaiseTicket(int raisedTo, int raisedBy, string ticket, string attachment)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("CreateRaiseTicket", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.Add(new SqlParameter("@raised_to", raisedTo));
                    cmd.Parameters.Add(new SqlParameter("@raised_by", raisedBy));
                    cmd.Parameters.Add(new SqlParameter("@ticket", ticket));
                    cmd.Parameters.Add(new SqlParameter("@attachment", attachment));

                    // Open the connection and execute the command
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}