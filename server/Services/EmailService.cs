using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using server.Interfaces;

namespace server.Services
{

    public class EmailSettings
    {
        public required string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public required string FromEmail { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.FromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            using (var smtpClient = new SmtpClient(emailSettings.SmtpServer, emailSettings.SmtpPort))
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }

}