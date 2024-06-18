using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace EmpLeavesTask
{
    public class EmailManager
    {
        public static bool SendEmailWithPDF(string email,string filepath)
        {
            string fromEmail = Constants.EMAILID;
            string fileName = $"{email}.pdf";
            string subject = "Letter";
            string body = "Dear Candidate,\n\nPlease find your Attchment.\n\nBest regards,\nYour Company";
            //string directoryPath = Server.MapPath("~/Payslips/");
            //string filePath = Path.Combine(directoryPath, fileName);

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;
                if (filepath != null) { 
                    mail.Attachments.Add(new Attachment(filepath));
                }

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(Constants.EMAILID, Constants.PASSKEY);
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Success", "alert('Offer letter emailed successfully.');", true);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('Error sending email: {ex.Message}');", true);
                        return false;
                    }
                }
            }

        }


        internal static void SendOtpEmail(string email, string otp)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(Constants.EMAILID);
                mail.To.Add(email);
                mail.Subject = "Your OTP Code";
                mail.Body = $"Your OTP code is {otp}";
                mail.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(Constants.EMAILID, Constants.PASSKEY);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

    }
}