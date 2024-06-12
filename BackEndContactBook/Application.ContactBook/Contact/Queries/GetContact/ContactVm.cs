
using Application.ContactProfiles.Queries.GetContactProfiles;

namespace Application.ContactProfiles.Queries.GetContacts
{
    public class ContactVm
    {
        public IReadOnlyCollection<ContactDto> Lists { get; init; } = Array.Empty<ContactDto>();

    }
}



