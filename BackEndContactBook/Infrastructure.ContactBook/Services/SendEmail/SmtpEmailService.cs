using Application.ContactBook.Common.Interfaces;
using Application.ContactBook.Common.Models;
using Domain.ContactBook.Entities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
namespace Infrastructure.ContactBook.Services.SendEmail
{
    internal class SmtpEmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpSettings _smtpSettings;

        public SmtpEmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
            _smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl
            };
        }


        public async Task SendEmailAsync(Email email)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.Username),
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email.To);
                if (!string.IsNullOrEmpty(email.Cc)) mailMessage.CC.Add(email.Cc);
                if (!string.IsNullOrEmpty(email.Bcc)) mailMessage.Bcc.Add(email.Bcc);

                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, rethrow, etc.)
                throw new InvalidOperationException("Failed to send email", ex);
            }
        }
    }
}
