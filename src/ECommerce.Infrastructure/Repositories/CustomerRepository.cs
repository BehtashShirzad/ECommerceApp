using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Domain.Aggregates.Customer.ValueObjects;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CustomerRepository(ApplicationDbContext context) :BaseRepository<Customer,CustomerId>(context),ICustomerRepository
{
    
    
}