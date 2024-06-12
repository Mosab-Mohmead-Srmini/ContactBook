using Application.ContactBook.Common.Interfaces;
using Application.UserProfiles.Queries.GetUserProfiles;
using Application.UserProfiles.Queries.GetUsers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactBook.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UsersPro.Queries.GetUsers
{
    public record UserQuery(int PageNumber, int PageSize) : IRequest<UserVm>;

    public class GetUserQueryHandler : IRequestHandler<UserQuery, UserVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserVm> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users
                .AsNoTracking()
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.FirstName);

            var paginatedUsers = await PaginatedList<UserDto>.CreateAsync(query, request.PageNumber, request.PageSize);

            return new UserVm
            {
                Lists = paginatedUsers
            };
        }
    }
}
