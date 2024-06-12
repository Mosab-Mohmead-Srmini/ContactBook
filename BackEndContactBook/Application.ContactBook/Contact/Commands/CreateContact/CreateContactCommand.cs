using Application.ContactBook.Common.Interfaces;
using ContactBook.Domain.Entities;
using Domain.ContactBook.Entities;
using Domain.ContactBook.Enums;
using MediatR;

namespace ContactBook.Application.Contacts.Commands.CreateContact;

public record CreateContactCommand : IRequest<int>
{
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
    public int UserId { get; set; }

}

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateContactCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var entity = new Contact();

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Email = request.Email;
        entity.Phone = request.Phone;
        entity.Email2 = request.Email2;
        entity.Mobile = request.Mobile;
        entity.Address = request.Address;
        entity.Address2 = request.Address2;
        entity.Image = request.Image;
        entity.StatusContact = StatusContact.Inactive;
        entity.UserId = request.UserId;


        _context.Contacts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
