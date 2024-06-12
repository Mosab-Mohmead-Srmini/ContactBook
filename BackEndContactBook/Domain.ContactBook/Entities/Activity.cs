using ContactBook.Domain.Common;
using ContactBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ContactBook.Entities
{
    public class Activity : BaseAuditableEntity
    {
        public string Action { get; set; }
        public int ByUserId { get; set; }
        public DateTime Date { get; set; }
        public int ContactId { get; set; }

        // Navigation property to the user who performed the action
        public virtual User ByUser { get; set; }

        // Navigation property to the contact involved in the action
        public virtual Contact Contact { get; set; }
    }
}
