using Application.ContactBook.Common.Interfaces;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Enums;
using Domain.ContactBook.Entities;
using Domain.ContactBook.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBook.Application.ContactInformations.Commands.RegisterNewUser
{
    public record RegisterNewUserCommand : IRequest<int>
    {
        // User information
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Company profile information
        public string CompanyName { get; set; } = string.Empty;
        public int VATNumber { get; set; } = 0;
        public string Street { get; set; } = string.Empty;
        public string Street2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int Zip { get; set; } = 0;
        public string Country { get; set; } = string.Empty;
    }

    public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public RegisterNewUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            // Create the CompanyProfile entity
            var companyProfile = new CompanyProfile
            {
                CompanyName = request.CompanyName,
                VATNumber = request.VATNumber,
                Street = request.Street,
                Street2 = request.Street2,
                City = request.City,
                State = request.State,
                Zip = request.Zip,
                Country = request.Country
            };

            // Add the CompanyProfile entity to the context and save it to generate the Id
            _context.CompanyProfiles.Add(companyProfile);
            await _context.SaveChangesAsync(cancellationToken);

            // Create the User entity
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                CompanyProfileId = companyProfile.Id,
                UserType = UserRole.RegularUser
            };

            // Add the User entity to the context and save it
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
