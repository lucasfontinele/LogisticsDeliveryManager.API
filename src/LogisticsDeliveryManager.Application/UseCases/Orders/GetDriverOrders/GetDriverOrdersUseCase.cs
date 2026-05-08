using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Orders;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.GetDriverOrders;

public interface IGetDriverOrdersUseCase
{
    Task<IEnumerable<Order>> Execute(Guid driverId);
}

public class GetDriverOrdersUseCase : IGetDriverOrdersUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetDriverOrdersUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> Execute(Guid driverId)
    {
        return await _orderRepository.GetAllByDriverId(driverId);
    }
}
