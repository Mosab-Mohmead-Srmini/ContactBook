using ContactBook.Application.Common.Mappings;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Enums;

namespace Application.UserProfiles.Queries.GetUsers
{
    public class UserItemDto : IMapFrom<User>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string UserRole { get; set; } = string.Empty;

        // Add the enum property
        public StatusUser UserStatus { get; set; } = StatusUser.Pending;

    }
}
