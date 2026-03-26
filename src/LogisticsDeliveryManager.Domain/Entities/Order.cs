namespace LogisticsDeliveryManager.Domain.Entities;

using LogisticsDeliveryManager.Domain.Enums;

public class Order
{
    public long Id { get; set; }
    public Customer Customer { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsPriority { get; set; }
}
