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
    public partial class User : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString);
        }

        protected void User_Submit(object sender, EventArgs e)
        {
            int id;
            string name = TextBox1.Text;
            string email = TextBox2.Text;
            string contact = TextBox3.Text;
            string doj = TextBox4.Text;

            string query = $"EXEC CreateEmployee '{name}','{email}','{contact}','{doj}'";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                conn.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            //Response.Write($"{id} {name} {email} {contact} {doj}");
            Response.Redirect($"LeaveApplication.aspx?id={id}");
        }
    }
}