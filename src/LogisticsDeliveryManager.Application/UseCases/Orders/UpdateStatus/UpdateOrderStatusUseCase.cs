using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Orders;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.UpdateStatus;

public class UpdateOrderStatusUseCase : IUpdateOrderStatusUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderStatusUseCase(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UpdateOrderStatusCommand command)
    {
        var order = await _orderRepository.GetById(command.OrderId);
        if (order is null)
            throw new ErrorOnValidationException(["Order not found."]);

        switch (command.NewStatus)
        {
            case OrderStatus.Delivered:
                order.ConfirmDelivery();
                break;
            case OrderStatus.Cancelled:
                order.CancelOrder();
                break;
            default:
                throw new ErrorOnValidationException(new List<string> { "Status transition must use a domain-specific action." });
        }

        _orderRepository.Update(order);
        await _unitOfWork.Commit();
    }
}
