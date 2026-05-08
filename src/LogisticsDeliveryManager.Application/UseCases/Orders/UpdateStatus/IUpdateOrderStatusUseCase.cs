using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.UpdateStatus;

public class UpdateOrderStatusCommand
{
    public Guid OrderId { get; set; }
    public OrderStatus NewStatus { get; set; }

    public UpdateOrderStatusCommand(Guid orderId, OrderStatus newStatus)
    {
        OrderId = orderId;
        NewStatus = newStatus;
    }
}

public interface IUpdateOrderStatusUseCase
{
    Task Execute(UpdateOrderStatusCommand command);
}
