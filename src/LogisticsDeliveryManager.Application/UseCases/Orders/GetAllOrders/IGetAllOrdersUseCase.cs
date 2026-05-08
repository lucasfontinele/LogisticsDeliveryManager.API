using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.GetAllOrders;

public interface IGetAllOrdersUseCase
{
    Task<IEnumerable<Order>> Execute();
}
