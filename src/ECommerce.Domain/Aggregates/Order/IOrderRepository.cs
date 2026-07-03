using ECommerce.Domain.Aggregates.Order.ValueObjects;
using ECommerce.Domain.Core;

namespace ECommerce.Domain.Aggregates.Order;

public interface IOrderRepository:IRepository<Order,OrderId>
{
    
}