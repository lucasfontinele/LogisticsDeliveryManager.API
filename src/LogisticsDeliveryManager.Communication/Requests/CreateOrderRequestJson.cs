using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Requests;

public class CreateOrderRequestJson
{
    public Guid CustomerId { get; set; }
    public required AddressRequestJson DestinationAddress { get; set; }
    public CargoTypeJson CargoType { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    public bool IsPriority { get; set; }
}
