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
    public partial class addemp : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeEmail = txtEmployeeEmail.Text;
            decimal employeeContact;
            string doj = txtDOJ.Text;

            if (!decimal.TryParse(txtEmployeeContact.Text, out employeeContact))
            {
                ltAddEmployeeMessage.Text = "<div class='alert alert-danger'>Invalid contact number.</div>";
                return;
            }

            if (!string.IsNullOrEmpty(employeeName) && !string.IsNullOrEmpty(employeeEmail) && !string.IsNullOrEmpty(doj))
            {
                bool isSuccess = AddEmployee(employeeName, employeeEmail, employeeContact, doj);
                if (isSuccess)
                {
                    ltAddEmployeeMessage.Text = "<div class='alert alert-success'>Employee added successfully!</div>";
                    txtEmployeeName.Text = string.Empty;
                    txtEmployeeEmail.Text = string.Empty;
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
                ltAddEmployeeMessage.Text = "<div class='alert alert-danger'>Please fill in all fields.</div>";
            }
        }


        public bool AddEmployee(string name, string email, decimal contact, string doj)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Employee (ename, email, contact, doj) VALUES (@Name, @Email, @Contact, @DOJ)", con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    cmd.Parameters.AddWithValue("@DOJ", doj);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}