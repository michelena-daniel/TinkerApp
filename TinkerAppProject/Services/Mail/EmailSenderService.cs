using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using TinkerAppProject.Models.Mail;

namespace TinkerAppProject.Services.Mail
{
    public class EmailSenderService(IOptions<MailAuthOptions> options, ILogger<EmailSenderService> logger) : IEmailSender
    {
        private readonly ILogger _logger = logger;
        private MailAuthOptions _options = options.Value;

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(_options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(_options.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("tinkerapplicationmail@gmail.com", "Tinker Application"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}
