using Application.ContactBook.Common.Interfaces;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Enums;
using Domain.ContactBook.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContactBook.Application.Users.Commands.InviteNewUser;

public record InviteNewUserCommand : IRequest<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
   // public string InvitedBy { get; set; }
    public UserRole UserRole { get; set; } = UserRole.RegularUser;

}

public class InviteNewUserCommandHandler : IRequestHandler<InviteNewUserCommand, int>
{
    private readonly IApplicationDbContext _context;
    private IHttpContextAccessor _httpContextAccessor;

    public InviteNewUserCommandHandler(IApplicationDbContext context , IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<int> Handle(InviteNewUserCommand request, CancellationToken cancellationToken)
    {

        // Get the logged-in user's ID from the claims
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            throw new UnauthorizedAccessException("Unable to identify the logged-in user.");
        }

        // Check if the user is allowed to invite new users
        var invitingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.UserType == UserRole.Admin, cancellationToken);

        if (invitingUser == null)
        {
            throw new UnauthorizedAccessException("You are not allowed to invite users.");
        }

        var entity = new User();

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Email = request.Email;
        entity.Phone = request.Phone;
        entity.Password = request.Password;
        entity.ConfirmPassword = request.ConfirmPassword;
        entity.UserStatus =StatusUser.Active;
        entity.UserType = UserRole.RegularUser;
        entity.CompanyProfileId = invitingUser.CompanyProfileId;
     //   entity.InvitedBy = request.InvitedBy;
        _context.Users.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
