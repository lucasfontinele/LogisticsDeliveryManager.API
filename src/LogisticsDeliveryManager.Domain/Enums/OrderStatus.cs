namespace LogisticsDeliveryManager.Domain.Enums;

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    InTransit,
    PickedUp,
    Delivered,
    Cancelled,
    PartialDelivered,
    Rescheduled,
    Returned,
    Redirected
}
