using Domain.ContactBook.Entities;

namespace Application.ContactBook.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
    }
}
