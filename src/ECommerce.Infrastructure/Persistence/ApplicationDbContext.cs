using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Aggregates.Category;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Domain.Aggregates.Order;
using ECommerce.Domain.Aggregates.Product;
using ECommerce.Domain.Core;
using ECommerce.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt,ICurrentUser currentUser,IPublisher bus) :
    IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(opt)
{
    readonly ICurrentUser  _currentUser=currentUser;
    readonly IPublisher  _domainEventBus=bus;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        base.OnModelCreating(modelBuilder);
        var assembly = InfrastructureLayerAssembly.Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        modelBuilder.Entity<AppUser>().ToTable("Users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
     
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userId =Guid.TryParse( _currentUser.UserId,out Guid currentUserId )? currentUserId  : Guid.Empty;

     


        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatorId = userId;
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifierId = userId;
                entry.Entity.ModifiedAt = DateTime.UtcNow;
            }
        }
  
        var domainEvents = ChangeTracker.Entries()
            .Where(e => e.Entity is IAggregateRoot)
            .SelectMany(e => ((IAggregateRoot)e.Entity).DomainEvents)
            .ToList();
 
        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _domainEventBus.Publish(domainEvent, cancellationToken);
        }

        foreach (var entry in ChangeTracker.Entries<IAggregateRoot>())
        {
            entry.Entity.ClearDomainEvents();
        }

        return result;
    }
    public DbSet<Customer> Customers=>Set<Customer>();
    public DbSet<Order> Orders=>Set<Order>();
    public DbSet<Category> Categories=>Set<Category>();
    public DbSet<Product> Products=>Set<Product>();

   
    
}