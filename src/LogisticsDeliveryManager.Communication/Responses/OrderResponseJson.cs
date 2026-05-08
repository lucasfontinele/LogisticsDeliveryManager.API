using LogisticsDeliveryManager.Communication.Enums;
using LogisticsDeliveryManager.Communication.Requests;

namespace LogisticsDeliveryManager.Communication.Responses;

public class OrderResponseJson
{
    public Guid Id { get; set; }
    public OrderStatusJson Status { get; set; }
    public Guid CustomerId { get; set; }
    public AddressRequestJson DestinationAddress { get; set; } = null!;
    public DateTime DeliveryWindowStart { get; set; }
    public DateTime DeliveryWindowEnd { get; set; }
    public CargoTypeJson CargoType { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    public bool IsPriority { get; set; }
    public Guid? AssignedVehicleId { get; set; }
    public int? Rating { get; set; }
    public string? Feedback { get; set; }
}
