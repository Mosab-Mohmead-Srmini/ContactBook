using ContactBook.Domain.Entities;
using MediatR;
using ContactBook.Application.Common.Exceptions;
using Application.ContactBook.Common.Interfaces;
using ContactBook.Domain.Enums;
using Domain.ContactBook.Enums;
namespace ContactBook.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest
{
    public int Id { get; init; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public UserRole UserType { get; set; } = UserRole.RegularUser;
    public StatusUser UserStatus { get; set; } = StatusUser.Pending;
    public int CompanyProfileId { get; set; }

}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    async Task<Unit> IRequestHandler<UpdateUserCommand, Unit>.Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Email = request.Email;
        entity.Phone = request.Phone;
        entity.UserType = UserRole.RegularUser;
        entity.UserStatus = StatusUser.Pending;
        entity.CompanyProfileId = request.CompanyProfileId;
     
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
