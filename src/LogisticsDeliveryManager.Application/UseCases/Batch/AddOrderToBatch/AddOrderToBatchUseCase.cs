using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Batches;
using LogisticsDeliveryManager.Domain.Repositories.Orders;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Batch.AddOrderToBatch;

public interface IAddOrderToBatchUseCase
{
    Task Execute(long batchId, long orderId);
}

public sealed class AddOrderToBatchUseCase : IAddOrderToBatchUseCase
{
    private readonly IBatchRepository _batchRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderToBatchUseCase(
        IBatchRepository batchRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _batchRepository = batchRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long batchId, long orderId)
    {
        var batch = await _batchRepository.GetById(batchId);
        if (batch is null)
            throw new NotFoundException("Batch not found.");

        var order = await _orderRepository.GetById(orderId);
        if (order is null)
            throw new NotFoundException("Order not found.");

        batch.AddOrder(order);

        _batchRepository.Update(batch);
        await _unitOfWork.Commit();
    }
}
