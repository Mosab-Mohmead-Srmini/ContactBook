using Application.ContactBook.Common.Interfaces;
using ContactBook.Domain.Entities;
using Domain.ContactBook.Entities;
using Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;


        public ApplicationDbContext(
             DbContextOptions<ApplicationDbContext> options,
             AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
             IMediator mediator
           ) : base(options)
        {
                _mediator = mediator;
                _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;

        }

        public DbSet<User> Users => Set<User>();
        
        public DbSet<CompanyProfile> CompanyProfiles => Set<CompanyProfile>();      

        public DbSet<Contact> Contacts => Set<Contact>();

        public DbSet<Activity> Activitys => Set<Activity>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyProfile>()
                .HasMany(c => c.Users)
                .WithOne(u => u.CompanyProfile)
                .HasForeignKey(u => u.CompanyProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Contacts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
