using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Batches;
using LogisticsDeliveryManager.Domain.Repositories.Orders;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Batch.AddOrderToBatch;

public interface IAddOrderToBatchUseCase
{
    Task Execute(Guid batchId, Guid orderId);
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

    public async Task Execute(Guid batchId, Guid orderId)
    {
        var batch = await _batchRepository.GetById(batchId);
        if (batch is null)
            throw new NotFoundException("Batch not found.");

        var order = await _orderRepository.GetById(orderId);
        if (order is null)
            throw new NotFoundException("Order not found.");

        batch.AddOrder(order.Id, order.Weight, order.Volume);
        order.AssignVehicle(batch.VehicleId);
        _orderRepository.Update(order);

        await _unitOfWork.Commit();
    }
}
