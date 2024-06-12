using ContactBook.Application.Common.Mappings;
using Domain.ContactBook.Entities;

namespace Application.ContactProfiles.Queries.GetContacts
{
    public class ContactItemDto : IMapFrom<Contact>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string Email2 { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string Address2 { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

    }
}
