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
    public partial class Login : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        int result;
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string uid = txtUsername.Text;
            string password = txtPassword.Text;

            if (isAdmin(uid, password))
            {
                // Redirect to a different page upon successful login
                Response.Redirect("HrFetchEmp.aspx");
            }
            else if (isValidUser(uid, out result))
            {
                Response.Redirect($@"EmpLeaveApply.aspx?empId={result}");
            }
            else
            {
                // Show an error message
                ltMessage.Text = "<div class='alert alert-danger'>Invalid username or password</div>";
            }
        }

        private bool isAdmin(string uid, string password)
        {
            // Replace with your actual user validation logic
            return uid == "admin" && password == "admin";
        }
        private bool isValidUser(string uid,out int result)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($@"SELECT emp_id FROM Employee WHERE email='{uid}'", con))
                {
                    con.Open();
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
            }
        }

    }
}