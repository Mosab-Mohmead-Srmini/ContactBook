using ContactBook.Domain.Common;
using ContactBook.Domain.Enums;
using Domain.ContactBook.Entities;
using Domain.ContactBook.Enums;

namespace ContactBook.Domain.Entities;

    public class User : BaseAuditableEntity
    {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public StatusUser UserStatus { get; set; } = StatusUser.Pending;
    public string InvitedBy { get; set; } = string.Empty;
    public UserRole UserType { get; set; } = UserRole.RegularUser ;
    public string Mobile { get; set; } = string.Empty;


    // Foreign key for CompanyProfile
    public int CompanyProfileId { get; set; }

    // Navigation property for the company profile
    public virtual CompanyProfile CompanyProfile { get; set; } 

     

    // Navigation property for related activities
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
