using ECommerce.Domain.Aggregates.Customer;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : DbContext(opt)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        base.OnModelCreating(modelBuilder);
        var assembly = InfrastructureLayerAssembly.Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }

    public DbSet<Customer> Customers=>Set<Customer>();
    
}