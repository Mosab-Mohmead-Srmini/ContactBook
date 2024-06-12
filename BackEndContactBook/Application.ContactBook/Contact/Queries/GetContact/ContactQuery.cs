using Application.ContactBook.Common.Interfaces;
using Application.ContactProfiles.Queries.GetContactProfiles;
using Application.ContactProfiles.Queries.GetContacts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ContactsPro.Queries.GetContacts
{
    public record ContactQuery : IRequest<ContactVm>;

    public class GetContactQueryHandler : IRequestHandler<ContactQuery, ContactVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetContactQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContactVm> Handle(ContactQuery request, CancellationToken cancellationToken)
        {
            return new ContactVm
            {
                Lists = await _context.Contacts
                    .AsNoTracking()
                    .ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.FirstName)
                    .ToListAsync(cancellationToken)
            };
        }
    }

}