using Application.ContactBook.Common.Interfaces;
using ContactBook.Domain.Entities;
using Domain.ContactBook.Enums;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
namespace Infrastructure.ContactBook.Identity
{
    public class UserRepositorySignIn : IUserRepositorySignIn
    {
        private readonly IApplicationDbContext _dbContext;

        public UserRepositorySignIn(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UniqeEmail(string email)
        {
            var uniqeEmail = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (uniqeEmail != null) return true; return false;
        }

        public async Task<bool> CanInviteUser()
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u =>u.UserType == UserRole.Admin);

            return user != null;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        }
    }
}
