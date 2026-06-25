using ECommerce.Domain.Aggregates.Category;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Domain.Aggregates.Order;
using ECommerce.Domain.Aggregates.Product;
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
    public DbSet<Order> Orders=>Set<Order>();
    public DbSet<Category> Categories=>Set<Category>();
    public DbSet<Product> Products=>Set<Product>();
    
}