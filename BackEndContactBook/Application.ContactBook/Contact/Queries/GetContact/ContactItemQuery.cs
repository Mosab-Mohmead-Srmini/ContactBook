using Application.ContactBook.Common.Interfaces;
using Application.ContactProfiles.Queries.GetContacts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ContactProfiles.Queries.GetContactProfiles
{
    public record ContactItemQuery(int ItemId) : IRequest<ContactItemDto>;

    public class ContactItemQueryHandler : IRequestHandler<ContactItemQuery, ContactItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContactItemQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ContactItemDto> Handle(ContactItemQuery request, CancellationToken cancellationToken)
        {
            var ContactItemDto = await _context.Contacts
            .AsNoTracking()
            .Where(l => l.Id == request.ItemId)
            .ProjectTo<ContactItemDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.FirstName)
            .FirstOrDefaultAsync(cancellationToken);

            if (ContactItemDto == null) return null;

            return ContactItemDto;
        }
    }
}

