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
    public partial class FetchEmp : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnFetchEmployee_Click(object sender, EventArgs e)
        {
            int employeeId;
            if (int.TryParse(txtEmployeeId.Text, out employeeId))
            {
                FetchEmployeeById(employeeId);
            }
            else
            {
                ltFetchEmployeeMessage.Text = "<div class='alert alert-danger'>Invalid Employee ID.</div>";
            }
        }

        private void FetchEmployeeById(int employeeId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE emp_id = @EmployeeId", con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string employeeDetails = $@"
                        <div class='card'>
                            <div class='card-body'>
                                <h5 class='card-title'>Employee ID: {reader["emp_id"]}</h5>
                                <p class='card-text'>Name: {reader["ename"]}</p>
                                <p class='card-text'>Email: {reader["email"]}</p>
                                <p class='card-text'>Contact: {reader["contact"]}</p>
                                <p class='card-text'>Date of Joining: {reader["doj"]}</p>
                            </div>
                        </div>";
                        ltEmployeeDetails.Text = employeeDetails;
                        /*if (employee != null)
                        {
                            ltEmployeeDetails.Text = $"Employee ID: {employee.EmpId}, Email: {employee.Email}";*/
                            PanelActionButtons.Visible = true; // Show the action buttons
                        /*}
                        else
                        {
                            ltEmployeeDetails.Text = "Employee not found.";
                            PanelActionButtons.Visible = false; // Hide the action buttons
                        }*/
                    }
                    else
                    {
                        PanelActionButtons.Visible = false;
                        ltEmployeeDetails.Text = "<div class='alert alert-warning'>No employee found with the given ID.</div>";
                    }
                }
            }
        }
        protected void btnGenerateOffer_Click(object sender, EventArgs e)
        {
            Response.Redirect($@"HrOfferLetter.aspx?empId={txtEmployeeId.Text}");

        }
    }
}