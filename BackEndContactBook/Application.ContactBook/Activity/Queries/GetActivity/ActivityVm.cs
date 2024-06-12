
using Application.ActivityProfiles.Queries.GetActivityProfiles;
using Application.UserProfiles.Queries.GetUserProfiles;
using ContactBook.Application.Common.Models;

namespace Application.ActivityProfiles.Queries.GetActivitys
{
    public class ActivityVm
    {
        public PaginatedList<ActivityDto> Lists { get; set; }
      

    }
}



