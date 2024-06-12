using ContactBook.Domain.Entities;
using MediatR;
using ContactBook.Application.Common.Exceptions;
using Application.ContactBook.Common.Interfaces;
using Domain.ContactBook.Entities;
using Domain.ContactBook.Enums;
namespace ContactBook.Application.Contacts.Commands.UpdateContact;

public record UpdateContactCommand : IRequest
{
    public int Id { get; init; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public string Email2 { get; set; } = string.Empty;

    public string Mobile { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public string Address2 { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;

    public StatusContact StatusContact { get; set; }    

}

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateContactCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    async Task<Unit> IRequestHandler<UpdateContactCommand, Unit>.Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Contacts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Contact), request.Id);
        }

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Email = request.Email;
        entity.Phone = request.Phone;
        entity.Email2 = request.Email2;
        entity.Mobile = request.Mobile;
        entity.Address = request.Address;
        entity.Address2 = request.Address2;
        entity.Image = request.Image;
        entity.StatusContact = request.StatusContact;


        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
