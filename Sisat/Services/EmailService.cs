using System.Net.Mail;
using System.Net;
using Sisat.Services.InterfaceService;

namespace Sisat.Service
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _sender;

        public EmailService(IConfiguration configuration)
        {
            _smtpClient = new SmtpClient(configuration["EmailSettings:MailServer"])
            {
                Port = int.Parse(configuration["EmailSettings:MailPort"]),
                Credentials = new NetworkCredential(configuration["EmailSettings:Sender"], configuration["EmailSettings:Password"]),
                EnableSsl = true,
            };
            _sender = configuration["EmailSettings:Sender"];
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_sender),
                Subject = subject,
                Body = htmlContent,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }

}