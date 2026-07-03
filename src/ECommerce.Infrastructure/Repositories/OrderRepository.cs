using ECommerce.Domain.Aggregates.Order;
using ECommerce.Domain.Aggregates.Order.ValueObjects;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class OrderRepository(ApplicationDbContext context) :BaseRepository<Order,OrderId>(context),IOrderRepository
{
    
}