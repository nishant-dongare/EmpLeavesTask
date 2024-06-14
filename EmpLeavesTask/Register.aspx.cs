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
    public partial class Register : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeEmail = txtEmployeeEmail.Text;
            string employeePass = txtPassword.Text;
            decimal employeeContact;
            string doj = txtDOJ.Text;
            string role = ddlRole.SelectedValue;

            if (!decimal.TryParse(txtEmployeeContact.Text, out employeeContact))
            {
                ltAddEmployeeMessage.Text = "<div class='alert alert-danger'>Invalid contact number.</div>";
                return;
            }

            if (!string.IsNullOrEmpty(employeeName) && !string.IsNullOrEmpty(employeeEmail) && !string.IsNullOrEmpty(doj) && !string.IsNullOrEmpty(employeePass))
            {
                bool isSuccess = AddEmployee(employeeName, employeeEmail, employeePass, employeeContact, doj,role);
                if (isSuccess)
                {
                    ltAddEmployeeMessage.Text = "<div class='alert alert-success'>Employee added successfully!</div>";
                    txtEmployeeName.Text = string.Empty;
                    txtEmployeeEmail.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtEmployeeContact.Text = string.Empty;
                    txtDOJ.Text = string.Empty;
                }
                else
                {
                    ltAddEmployeeMessage.Text = "<div class='alert alert-danger'>Failed to add employee.</div>";
                }
            }
            else
            {
                ltAddEmployeeMessage.Text = "<div class='alert alert-danger'>Please fill in all fields correct</div>";
            }
        }

        public bool AddEmployee(string name, string email, string pass, decimal contact, string doj,string role)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($@"EXEC AddEmployee '{name}','{email}','{pass}','{contact}','{doj}','{role}'", con))
                {
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}