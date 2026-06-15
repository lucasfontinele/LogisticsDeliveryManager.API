using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Services.Orders;

public class DeliveryPromiseService : IDeliveryPromiseService
{
    private static readonly TimeSpan PriorityDeliveryHorizon = TimeSpan.FromDays(1);
    private static readonly TimeSpan StandardDeliveryHorizon = TimeSpan.FromDays(3);

    public DeliveryWindow CalculateDeliveryPromise(bool isPriority)
    {
        var start = DateTime.UtcNow;
        var end = start.Add(isPriority ? PriorityDeliveryHorizon : StandardDeliveryHorizon);
        return new DeliveryWindow(start, end);
    }

    public void ValidateDeliveryWindow(DeliveryWindow deliveryWindow)
    {
        if (deliveryWindow == null)
            throw new ErrorOnValidationException(new List<string> { "Delivery window is required." });

        if (deliveryWindow.Start >= deliveryWindow.End)
            throw new ErrorOnValidationException(new List<string> { "Delivery window start must be before end." });
    }
}
