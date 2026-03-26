using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Batch
{
    public long Id { get; set; }
    public CargoType Type { get; set; }
    public Driver Driver { get; set; }
    public Vehicle Vehicle { get; set; }
    public DateOnly DeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
