using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class ShippingStatuses : EntityBase
{
    public Shipping Shipping { get; private set; }
    public DateOnly EstimatedDeliveryDate { get; private set; }
    public OrderStatus Status { get; private set; }

    private ShippingStatuses() { }

    public ShippingStatuses(Shipping shipping, DateOnly estimatedDeliveryDate, OrderStatus status)
    {
        Validate(shipping, estimatedDeliveryDate, status);
        Shipping = shipping;
        EstimatedDeliveryDate = estimatedDeliveryDate;
        Status = status;
    }

    private static void Validate(Shipping shipping, DateOnly estimatedDeliveryDate, OrderStatus status)
    {
        var errors = new List<string>();

        if (shipping == null)
            errors.Add("Shipping cannot be null.");

        if (estimatedDeliveryDate < DateOnly.FromDateTime(DateTime.UtcNow))
            errors.Add("Estimated delivery date cannot be in the past.");

        if (!Enum.IsDefined(typeof(OrderStatus), status))
            errors.Add("Invalid order status.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
    