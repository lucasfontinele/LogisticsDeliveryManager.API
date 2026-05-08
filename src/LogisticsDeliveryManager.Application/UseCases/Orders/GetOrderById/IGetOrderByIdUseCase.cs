using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.GetOrderById;

public interface IGetOrderByIdUseCase
{
    Task<Order?> Execute(long id);
}
