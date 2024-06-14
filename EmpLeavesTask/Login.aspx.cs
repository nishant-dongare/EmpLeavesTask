﻿using System;
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
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string uid = txtUsername.Text;
            string pass = txtPassword.Text;
            string role = isValidUser(uid, pass);
            if (role == null)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Invalid username or password</div>";
            }
            else if (role == "hr")
            {
                Response.Redirect("HrFetchEmp.aspx");
            }
            else
            {
                Response.Redirect($@"EmpLeaveApply.aspx");
            }
        }

        private string isValidUser(string uid, string passkey)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($@"EXEC AuthLogin '{uid}','{passkey}'", conn))
                {
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows) { 
                        rdr.Read();
                        Session["emp_id"] = rdr["emp_id"];
                        Session["email"] = rdr["email"];
                        return rdr["erole"].ToString();
                    }
                    return null;
                }
            }
        }

        /*private bool isValidUser(string uid,string passkey,out int result)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand($@"SELECT emp_id FROM Employee WHERE email='{uid}' AND passkey='{passkey}'", con))
                {
                    con.Open();
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
            }
        }*/
    }
}