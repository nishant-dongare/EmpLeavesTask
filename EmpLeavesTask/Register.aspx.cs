/*using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "showOtpModal", "$('#otpModal').modal('show');", true);


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

        private string GenerateOTP()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999).ToString(); // Generate a 6-digit OTP
        }

        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            string enteredOTP = txtOTP.Text;

            if (VerifyOTP(enteredOTP))
            {
                // Proceed with the registration process
                // Add your registration logic here

                ltAddEmployeeMessage.Text = "Employee registered successfully.";
            }
            else
            {
                ltAddEmployeeMessage.Text = "Invalid OTP. Please try again.";
                ScriptManager.RegisterStartupScript(this, GetType(), "showOtpModal", "$('#otpModal').modal('show');", true);
            }
        }

        private bool VerifyOTP(string enteredOTP)
        {
            // Replace this logic with your actual OTP verification logic
            const string actualOTP = "123456";

            return enteredOTP == actualOTP;
        }


        public void reset()
        {
            txtEmployeeName.Text = "";
            txtEmployeeEmail.Text = "";
            txtEmployeeContact.Text = "";
            txtPassword.Text = "";
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpLeavesTask
{
    public partial class Register : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        private static string generatedOTP; // Static field to hold the generated OTP

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
                // Generate and send OTP
                generatedOTP = GenerateOTP();
                Session["otp"] = generatedOTP;
                // You should implement a function to send the OTP to the user's email or phone here
                EmailManager.SendOtpEmail(employeeEmail, generatedOTP);

                // Show OTP modal
                ScriptManager.RegisterStartupScript(this, GetType(), "showOtpModal", "$('#otpModal').modal('show');", true);
            }
            else
            {
                ltAddEmployeeMessage.Text = "<div class='alert alert-danger'>Please fill in all fields correctly</div>";
            }
        }

        public bool AddEmployee(string name, string email, string pass, decimal contact, string doj, string role)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand($"EXEC AddEmployee '{name}', '{email}', '{pass}', '{contact}', '{doj}', '{role}'", con))
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2627) // 2627 is the error number for unique key violation
            {
                ltAddEmployeeMessage.Text = "An employee with this email already exists.";
                return false;
            }
        }

        private string GenerateOTP()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999).ToString(); // Generate a 6-digit OTP
        }

        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            string enteredOTP = txtOTP.Text;
            //generatedOTP = GenerateOTP();
            //Session["otp"] = generatedOTP;

            if (VerifyOTP(enteredOTP))
            {
                // Proceed with the registration process
                string employeeName = txtEmployeeName.Text;
                string employeeEmail = txtEmployeeEmail.Text;
                string employeePass = txtPassword.Text;
                decimal employeeContact;
                string doj = txtDOJ.Text;
                string role = ddlRole.SelectedValue;

                if (decimal.TryParse(txtEmployeeContact.Text, out employeeContact))
                {
                    string hashedPassword = HashPassword(employeePass);
                    bool isSuccess = AddEmployee(employeeName, employeeEmail, hashedPassword, employeeContact, doj, role);

                    if (isSuccess)
                    {
                        ltAddEmployeeMessage.Text = "<div class='alert alert-success'>Employee registered successfully.</div>";
                        reset();
                    }
                    else
                    {
                        ltAddEmployeeMessage.Text = "<div class='alert alert-danger'>Failed to add employee.</div>";
                    }
                }
            }
            else
            {
                ltAddEmployeeMessage.Text = "<div class='alert alert-danger'>Invalid OTP. Please try again.</div>";
                ScriptManager.RegisterStartupScript(this, GetType(), "showOtpModal", "$('#otpModal').modal('show');", true);
            }
        }

        private bool VerifyOTP(string enteredOTP)
        {
            return enteredOTP == Session["otp"].ToString();
        }

        public void reset()
        {
            txtEmployeeName.Text = "";
            txtEmployeeEmail.Text = "";
            txtEmployeeContact.Text = "";
            txtPassword.Text = "";
            txtDOJ.Text = "";
        }

        internal static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password.Trim()));
                StringBuilder builder = new StringBuilder();
                foreach (byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
