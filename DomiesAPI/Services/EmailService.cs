using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace DomiesAPI.Services
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string recipientEmail, string subject, string body);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationEmailAsync(string recipientEmail, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            string host = smtpSettings["Host"];
            int port = int.Parse(smtpSettings["Port"]);
            string username = smtpSettings["Username"];
            string password = smtpSettings["Password"];
            bool enableSsl = bool.Parse(smtpSettings["EnableSsl"]);


            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Domies", username));
            mailMessage.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("html")
            {
                Text = body
            };

            // wysyłanie 
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(username, password);
                    await smtpClient.SendAsync(mailMessage);
                    Console.WriteLine("Email został wysłany ");
                    await smtpClient.DisconnectAsync(true);

                }             
                catch (Exception ex) 
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
