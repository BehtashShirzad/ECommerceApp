using ECommerce.Domain.Aggregates.Customer.ValueObjects;
using ECommerce.Domain.Core;

namespace ECommerce.Domain.Aggregates.Customer;

public interface ICustomerRepository:IRepository<Customer,CustomerId>
{
    public Task<IReadOnlyCollection<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
}