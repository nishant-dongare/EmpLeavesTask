using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (IsValidUser(username, password))
            {
                // Redirect to a different page upon successful login
                Response.Redirect("addemp.aspx");
            }
            else
            {
                // Show an error message
                ltMessage.Text = "<div class='alert alert-danger'>Invalid username or password</div>";
            }
        }

        private bool IsValidUser(string username, string password)
        {
            // Replace with your actual user validation logic
            return username == "admin" && password == "password";
        }
    }
}