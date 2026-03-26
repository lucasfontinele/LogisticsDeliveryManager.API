using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities;

public class ShippingStatuses
{
    public long Id { get; set; }
    public long ShippingId { get; set; }
    public DateOnly EstimatedDeliveryDate { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
