using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Orders;

public interface IOrderRepository
{
    Task Add(Order order);
    Task<Order?> GetById(long id);
    Task<IEnumerable<Order>> GetAllByCustomerId(long customerId);
    void Update(Order order);
}
