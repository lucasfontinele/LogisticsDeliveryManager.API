using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Orders;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.GetCustomerOrders;

public interface IGetCustomerOrdersUseCase
{
    Task<IEnumerable<Order>> Execute(long customerId);
}

public class GetCustomerOrdersUseCase : IGetCustomerOrdersUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetCustomerOrdersUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> Execute(long customerId)
    {
        return await _orderRepository.GetAllByCustomerId(customerId);
    }
}
