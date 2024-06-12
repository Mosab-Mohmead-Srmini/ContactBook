using Application.ContactBook.Common.Interfaces;
using Application.UserProfiles.Queries.GetUsers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserProfiles.Queries.GetUserProfiles
{
    public record UserItemQuery(int ItemId) : IRequest<UserItemDto>;

    public class UserItemQueryHandler : IRequestHandler<UserItemQuery, UserItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserItemQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserItemDto> Handle(UserItemQuery request, CancellationToken cancellationToken)
        {
            var UserItemDto = await _context.Users
            .AsNoTracking()
            .Where(l => l.Id == request.ItemId)
            .ProjectTo<UserItemDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.FirstName)
            .FirstOrDefaultAsync(cancellationToken);

            if (UserItemDto == null) return null;

            return UserItemDto;
        }
    }
}

