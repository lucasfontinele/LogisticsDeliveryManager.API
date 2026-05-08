using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Orders;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.GetOrdersByCustomerId;

public interface IGetOrdersByCustomerIdUseCase
{
    Task<IEnumerable<Order>> Execute(long customerId);
}

public class GetOrdersByCustomerIdUseCase : IGetOrdersByCustomerIdUseCase
{
    private readonly IOrderRepository _repository;

    public GetOrdersByCustomerIdUseCase(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> Execute(long customerId)
    {
        return await _repository.GetAllByCustomerId(customerId);
    }
}
