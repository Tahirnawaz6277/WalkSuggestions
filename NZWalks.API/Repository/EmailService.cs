
using NZWalks.API.Models.DTO.Email;
using NZWalks.API.Repository;
using System.Net;
using System.Net.Mail;

namespace NZWalks.API.Services
{
    public class EmailService : IEmail
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

     
        public void SendEmail(EmailDTO email)
        {
            // Set up SMTP server settings
            string smtpUsername = configuration["EmailSettings:Username"];
            string smtpPassword = configuration["EmailSettings:Password"];
            string smtpHost = configuration["EmailSettings:SmtpHost"];
            int smtpPort = configuration.GetValue<int>("EmailSettings:SmtpPort");
            bool enableSsl = configuration.GetValue<bool>("EmailSettings:EnableSsl");

            using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = enableSsl;

                // Create the email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(configuration["EmailSettings:Username"]); // Set sender's email address
                mailMessage.To.Add(email.to); // Set recipient's email address
                mailMessage.Subject = email.subject; // Set email subject
                mailMessage.IsBodyHtml = true; // Set body format to HTML

                // Set the email body
                mailMessage.Body = email.body;

                // Send the email
                smtpClient.Send(mailMessage);
            }
        }

    }
}

