using LogisticsDeliveryManager.Domain.ValueObjects;

namespace LogisticsDeliveryManager.Domain.Services.Orders;

public interface IDeliveryPromiseService
{
    DeliveryWindow CalculateDeliveryPromise(bool isPriority);
    void ValidateDeliveryWindow(DeliveryWindow deliveryWindow);
}
