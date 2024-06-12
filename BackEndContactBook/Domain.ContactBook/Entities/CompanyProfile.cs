using ContactBook.Domain.Common;
using ContactBook.Domain.Entities;

namespace Domain.ContactBook.Entities
{
    public class CompanyProfile : BaseAuditableEntity
    {
        public string CompanyName { get; set; } = string.Empty;
        public int VATNumber { get; set; } = 0;
        public string Street { get; set; } = string.Empty;
        public string Street2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int Zip { get; set; } = 0;
        public string Country { get; set; } = string.Empty;

        // Navigation property for related users
        public ICollection<User> Users { get; set; } = new List<User>();

        // Navigation property for related contacts
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    }
}
