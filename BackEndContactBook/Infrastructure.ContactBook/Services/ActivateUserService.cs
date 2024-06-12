using Application.ContactBook.Common.Interfaces;
using ContactBook.Application.Common.Exceptions;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ContactBook.Services
{
    public class ActivateUserService : IActivateUserService
    {
       
        private readonly IApplicationDbContext _context;
        private readonly IActivateUserService _activateUserService;

        public ActivateUserService(IApplicationDbContext context , IActivateUserService activateUserService)
        {
            _context = context;
            _activateUserService = activateUserService;
        }

        public async Task ActivateUserAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be empty or null...", nameof(email));
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), email);
            }
            user.UserStatus = StatusUser.Active;

            await _context.SaveChangesAsync(CancellationToken.None);

        }
    }
}
