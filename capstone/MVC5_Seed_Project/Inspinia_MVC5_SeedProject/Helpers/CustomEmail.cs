using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

namespace Inspinia_MVC5_SeedProject.Helpers
{
    public class CustomEmail
    {
        // think later if attributes & constructor is required
        //String mailMessage;

        public void sendMail(String toAddress, String subject, String mailMessage)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(toAddress));  // replace with valid value 
            message.From = new MailAddress("urvimalviya@gmail.com");  // replace with valid value
            message.Subject = subject;
            message.Body = string.Format(body, "Select International", "select@noreply", mailMessage);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "",  // replace with valid value
                    Password = ""  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                //smtp.Send(message);
            }
        }
    }
}