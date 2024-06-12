using Application.ContactBook.Common.Interfaces;
using Application.ContactBook.SendEmail.Queries;
using CleanArch.WebUI.Controllers;
using Domain.ContactBook.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ContactBook.Controllers
{

    public class EmailController : ApiControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("Contacts/SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto emailDto)
        {
            var email = new Email
            {
                To = emailDto.To,
                Cc = emailDto.Cc,
                Bcc = emailDto.Bcc,
                Subject = emailDto.Subject,
                Body = emailDto.Body
            };

            await _emailService.SendEmailAsync(email);
            return Ok();
        }
    }
}
