using Application.ContactBook.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ContactBook.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public UserRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);


            // Check if user exists and the password is correct
            if (user != null && await VerifyPasswordAsync(password))
            {
                return true;
            }

            return false;
        }
        public async Task<bool> VerifyPasswordAsync(string password)
        {
            // Check if any user has the provided password
            var userWithPassword = await _dbContext.Users.AnyAsync(u => u.Password == password);
            return userWithPassword;
        }
        public async Task<int> GetUserIdAsync(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return user.Id;
            }
            return -1;
        }
    }
}
