using Application.ActivityProfiles.Queries.GetActivityProfiles;
using Application.ActivityProfiles.Queries.GetActivitys;
using Application.ContactBook.Common.Interfaces;
using AutoMapper;
using ContactBook.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ActivitysPro.Queries.GetActivitys
{
    public record ActivityQuery(int PageNumber, int PageSize) : IRequest<ActivityVm>;

    public class GetActivityQueryHandler : IRequestHandler<ActivityQuery, ActivityVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetActivityQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActivityVm> Handle(ActivityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _context.Activitys
                    .AsNoTracking()
                    .Include(a => a.Contact)
                    .Include(a => a.ByUser)
                    .Select(a => new ActivityDto
                    {
                        ContactFirstName = a.Contact.FirstName,
                        ContactLastName = a.Contact.LastName,
                        Action = a.Action,
                        Date = a.Date,
                    })
                    .OrderBy(a => a.ContactFirstName);

                var paginatedActivity = await PaginatedList<ActivityDto>.CreateAsync(query, request.PageNumber, request.PageSize);
                return new ActivityVm
                {
                    Lists = paginatedActivity
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ApplicationException("An error occurred while retrieving activities", ex);
            }
        }
    }
}
