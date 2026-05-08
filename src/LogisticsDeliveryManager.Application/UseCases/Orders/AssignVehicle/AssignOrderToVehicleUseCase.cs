using LogisticsDeliveryManager.Domain.Repositories.Orders;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.AssignVehicle;

public interface IAssignOrderToVehicleUseCase
{
    Task Execute(long orderId, long vehicleId);
}

public class AssignOrderToVehicleUseCase : IAssignOrderToVehicleUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignOrderToVehicleUseCase(
        IOrderRepository orderRepository,
        IVehicleRepository vehicleRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long orderId, long vehicleId)
    {
        var order = await _orderRepository.GetById(orderId);
        if (order is null)
            throw new NotFoundException("Order not found.");

        var vehicle = await _vehicleRepository.GetById(vehicleId);
        if (vehicle is null)
            throw new NotFoundException("Vehicle not found.");

        order.AssignVehicle(vehicle);

        _orderRepository.Update(order);
        await _unitOfWork.Commit();
    }
}
