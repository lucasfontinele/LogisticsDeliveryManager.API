namespace LogisticsDeliveryManager.Domain.Enums;

public enum OrderStatus
{
    Pending,
    Processing,
    PickedUp,
    Delivered,
    Cancelled,
    PartialDelivered,
    Rescheduled,
    Returned,
    Redirected
}
