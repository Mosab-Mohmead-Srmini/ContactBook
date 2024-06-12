using ContactBook.Domain.Entities;
using Domain.ContactBook.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.ContactBook.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<CompanyProfile> CompanyProfiles { get; }
        DbSet<Contact> Contacts { get; }
        DbSet<Activity> Activitys { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
