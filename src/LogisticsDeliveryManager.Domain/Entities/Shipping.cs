namespace LogisticsDeliveryManager.Domain.Entities;

public class Shipping
{
    public long Id { get; set; }
    public Order Order { get; set; }
    public string Address { get; set; }
    public DateOnly EstimatedDeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}