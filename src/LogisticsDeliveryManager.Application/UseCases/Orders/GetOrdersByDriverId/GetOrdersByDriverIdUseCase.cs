using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Orders;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.GetOrdersByDriverId;

public interface IGetOrdersByDriverIdUseCase
{
    Task<IEnumerable<Order>> Execute(Guid driverId);
}

public class GetOrdersByDriverIdUseCase : IGetOrdersByDriverIdUseCase
{
    private readonly IOrderRepository _repository;

    public GetOrdersByDriverIdUseCase(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> Execute(Guid driverId)
    {
        return await _repository.GetAllByDriverId(driverId);
    }
}
