using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities;

public class ShippingStatuses
{
    public long Id { get; set; }
    public Shipping Shipping { get; set; }
    public DateOnly EstimatedDeliveryDate { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
