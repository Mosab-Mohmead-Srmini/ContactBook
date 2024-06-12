using ContactBook.Domain.Entities;
using MediatR;
using ContactBook.Application.Common.Exceptions;
using Application.ContactBook.Common.Interfaces;
using Domain.ContactBook.Entities;
namespace ContactBook.Application.CompanyProfiles.Commands.UpdateCompanyProfile;

public record UpdateCompanyProfileCommand : IRequest
{
    public int Id { get; init; }

    public string CompanyName { get; set; } = string.Empty;

    public int VATNumber { get; set; } = 0;

    public string Street { get; set; } = string.Empty;

    public string Street2 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public int Zip { get; set; } = 0;
    public string Country { get; set; } = string.Empty;

}

public class UpdateCompanyProfileCommandHandler : IRequestHandler<UpdateCompanyProfileCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCompanyProfileCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    async Task<Unit> IRequestHandler<UpdateCompanyProfileCommand, Unit>.Handle(UpdateCompanyProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CompanyProfiles
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CompanyProfile), request.Id);
        }

        entity.CompanyName = request.CompanyName;
        entity.VATNumber = request.VATNumber;
        entity.Street = request.Street;
        entity.Street2 = request.Street2;
        entity.City = request.City;
        entity.State = request.State;
        entity.Zip = request.Zip;
        entity.Country = request.Country;


        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
