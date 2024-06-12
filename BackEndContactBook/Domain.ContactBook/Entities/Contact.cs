using ContactBook.Domain.Common;
using ContactBook.Domain.Entities;
using Domain.ContactBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ContactBook.Entities
{
    public class Contact : BaseAuditableEntity
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
        public StatusContact StatusContact { get; set; }

        // Foreign key to the User who owns this contact
        public int UserId { get; set; }

        // Navigation property to the User
        public virtual User User { get; set; }

        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
