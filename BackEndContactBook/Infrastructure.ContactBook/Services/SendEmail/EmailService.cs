using Application.ContactBook.Common.Interfaces;
using Domain.ContactBook.Entities;

namespace Infrastructure.ContactBook.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IEmailService _emailService;

        public EmailService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task SendEmailAsync(Email emailModel)
        {
            await _emailService.SendEmailAsync(emailModel);
        }
    }
}
