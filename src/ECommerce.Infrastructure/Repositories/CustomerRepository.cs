using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Domain.Aggregates.Customer.ValueObjects;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CustomerRepository(ApplicationDbContext context) :BaseRepository<Customer,CustomerId>(context),ICustomerRepository
{
    public async Task<IReadOnlyCollection<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await context.Customers.AsNoTracking().Include(_ => _.IdentityUser).AsSplitQuery()
            .ToListAsync(cancellationToken);
        return result.AsReadOnly();
    }
}