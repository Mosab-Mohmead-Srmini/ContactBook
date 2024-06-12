using Application.ContactBook.Common.Interfaces;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Enums;
using Domain.ContactBook.Entities;
using MediatR;

namespace ContactBook.Application.CompanyProfiles.Commands.CreateCompanyProfile;

public record CreateCompanyProfileCommand : IRequest<int>
{
    public string CompanyName { get; set; } = string.Empty;

    public int VATNumber { get; set; } = 0;

    public string Street { get; set; } = string.Empty;

    public string Street2 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public int Zip { get; set; } = 0;
    public string Country { get; set; } = string.Empty;

  
}

public class CreateCompanyProfileCommandHandler : IRequestHandler<CreateCompanyProfileCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCompanyProfileCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCompanyProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = new CompanyProfile();

        entity.CompanyName = request.CompanyName;
        entity.VATNumber= request.VATNumber;
        entity.Street = request.Street;
        entity.Street2 = request.Street2;
        entity.City = request.City;
        entity.State = request.State;
        entity.Zip = request.Zip;
        entity.Country = request.Country;


        _context.CompanyProfiles.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
