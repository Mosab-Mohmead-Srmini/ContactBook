using Application.ContactBook.Common.Interfaces;
using Application.CompanyProfileProfiles.Queries.GetCompanyProfileProfiles;
using Application.CompanyProfileProfiles.Queries.GetCompanyProfiles;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CompanyProfilesPro.Queries.GetCompanyProfiles
{
    public record CompanyProfileQuery : IRequest<CompanyProfileVm>;

    public class GetCompanyProfileQueryHandler : IRequestHandler<CompanyProfileQuery, CompanyProfileVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompanyProfileQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyProfileVm> Handle(CompanyProfileQuery request, CancellationToken cancellationToken)
        {
            return new CompanyProfileVm
            {
                Lists = await _context.CompanyProfiles
                    .AsNoTracking()
                    .ProjectTo<CompanyProfileDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.CompanyName)
                    .ToListAsync(cancellationToken)
            };
        }
    }

}