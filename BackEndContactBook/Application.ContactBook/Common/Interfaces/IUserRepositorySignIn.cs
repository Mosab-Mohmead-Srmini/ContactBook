using ContactBook.Domain.Entities;

namespace Application.ContactBook.Common.Interfaces
{
    public interface IUserRepositorySignIn
    {
        Task<bool> UniqeEmail(string email);

        Task<bool> CanInviteUser();

        Task<User> GetUserByIdAsync(string userId);
    }
}
