using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.CreateOrder;

public interface ICreateOrderUseCase
{
    Task<Order> Execute(CreateOrderCommand command);
}
