using Application.ContactBook.Common.Interfaces;
using Application.CompanyProfileProfiles.Queries.GetCompanyProfiles;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CompanyProfileProfiles.Queries.GetCompanyProfileProfiles
{
    public record CompanyProfileItemQuery(int ItemId) : IRequest<CompanyProfileItemDto>;

    public class CompanyProfileItemQueryHandler : IRequestHandler<CompanyProfileItemQuery, CompanyProfileItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CompanyProfileItemQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompanyProfileItemDto> Handle(CompanyProfileItemQuery request, CancellationToken cancellationToken)
        {
            var CompanyProfileItemDto = await _context.CompanyProfiles
            .AsNoTracking()
            .Where(l => l.Id == request.ItemId)
            .ProjectTo<CompanyProfileItemDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.CompanyName)
            .FirstOrDefaultAsync(cancellationToken);

            if (CompanyProfileItemDto == null) return null;

            return CompanyProfileItemDto;
        }
    }
}

