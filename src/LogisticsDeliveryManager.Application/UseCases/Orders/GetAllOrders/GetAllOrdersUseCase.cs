using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Orders;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.GetAllOrders;

public class GetAllOrdersUseCase : IGetAllOrdersUseCase
{
    private readonly IOrderRepository _repository;

    public GetAllOrdersUseCase(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Order>> Execute()
    {
        return await _repository.GetAll();
    }
}
