    using ContactBook.Application.Common.Mappings;
using Domain.ContactBook.Entities;

namespace Application.ActivityProfiles.Queries.GetActivityProfiles
{
    public class ActivityDto : IMapFrom<Activity>
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int ByUserId { get; set; }
        public DateTime Date { get; set; }
        public int ContactId { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }


    }

}
