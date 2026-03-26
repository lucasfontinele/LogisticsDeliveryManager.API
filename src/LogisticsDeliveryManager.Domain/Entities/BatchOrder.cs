using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities;

public class BatchOrder
{
    public long Id { get; set; }
    public Batch Batch { get; set; }
    public Order Order { get; set; }
    public DateTime CreatedAt { get; set; }
}
   
