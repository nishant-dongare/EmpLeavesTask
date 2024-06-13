using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EmpLeavesTask
{
    public partial class HrOfferLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlOfferLetter.Visible = false;
            }
        }


        protected void btnGenerateOfferLetter_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string contact = txtContact.Text;
            string doj = txtDateOfJoining.Text;

            string offerLetterContent = GenerateOfferLetter(name, email, contact, doj);

            pnlOfferLetter.Visible = true;
            pnlOfferLetter.Controls.Add(new LiteralControl(offerLetterContent));
            string s = SaveOfferLetter(name,email, contact, doj);
            //Response.Write(s);
            string filePath = "OfferLetters/" + email + ".pdf";

            //Stores into db
            SaveOfferLetterDetails(name, email,contact, doj, filePath);

            // Email the PDF here
            string directoryPath = Server.MapPath("~/OfferLetters/");
            string filepath1 = Path.Combine(directoryPath, email+".pdf");
            if (EmailManager.SendEmailWithPDF(email, filepath1))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Success", "alert('Offer letter emailed successfully.');", true);
            }
            else {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('Error sending email');", true);
            }
        }



        private string GenerateOfferLetter(string name, string email, string contact, string dateOfJoining)
        {
            // Generate offer letter content here
            string offerLetterContent = $"Dear Candidate {name},\n\nWe are pleased to offer you a position at our company.\n\nDate of Joining: {dateOfJoining}\n\nContact: {contact}\n\nSincerely,\nHR Department";

            // Convert offer letter content to PDF
            MemoryStream ms = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            document.Open();
            document.Add(new Paragraph(offerLetterContent));
            document.Close();

            byte[] bytes = ms.ToArray();
            string base64 = Convert.ToBase64String(bytes);
            return $"<embed type='application/pdf' width='100%' height='600px' src='data:application/pdf;base64,{base64}'/>";
        }

        private string SaveOfferLetter(string name,string email, string contact, string dateOfJoining)
        {
            // Generate offer letter content here
            string offerLetterContent = $"Dear Candidate {name},\n\nWe are pleased to offer you a position at our company.\n\nDate of Joining: {dateOfJoining}\n\nContact: {contact}\n\nSincerely,\nHR Department";

            // Define the file path where the PDF will be saved
            string filePath = Server.MapPath($"~/OfferLetters/{email}.pdf");

            // Ensure the directory exists
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Save the offer letter content to a PDF file
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();
                document.Add(new Paragraph(offerLetterContent));
                document.Close();
            }

            return filePath;
        }

        private void SaveOfferLetterDetails(string name, string email,string contact, string doj, string filePath)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO OfferLetters (name, email,contact, doj, filepath) VALUES (@Name, @Email,@Contact, @DOJ, @FilePath)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Contact", contact);
                    command.Parameters.AddWithValue("@DOJ", doj);
                    command.Parameters.AddWithValue("@FilePath", filePath);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
