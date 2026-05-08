using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Orders;

public interface IOrderRepository
{
    Task Add(Order order);
    Task<Order?> GetById(Guid id);
    Task<IEnumerable<Order>> GetAll();
    Task<IEnumerable<Order>> GetAllByCustomerId(Guid customerId);
    Task<IEnumerable<Order>> GetAllByDriverId(Guid driverId);
    void Update(Order order);
}
