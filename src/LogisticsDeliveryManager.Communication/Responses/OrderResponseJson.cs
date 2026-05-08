using LogisticsDeliveryManager.Communication.Enums;
using LogisticsDeliveryManager.Communication.Requests;

namespace LogisticsDeliveryManager.Communication.Responses;

public class OrderResponseJson
{
    public long Id { get; set; }
    public OrderStatusJson Status { get; set; }
    public long CustomerId { get; set; }
    public AddressRequestJson DestinationAddress { get; set; } = null!;
    public DateTime DeliveryWindowStart { get; set; }
    public DateTime DeliveryWindowEnd { get; set; }
    public CargoTypeJson CargoType { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    public bool IsPriority { get; set; }
    public long? AssignedVehicleId { get; set; }
}
