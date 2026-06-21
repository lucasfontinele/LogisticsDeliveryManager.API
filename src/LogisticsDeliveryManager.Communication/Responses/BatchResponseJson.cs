using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Responses;

public class BatchResponseJson
{
    public Guid Id { get; set; }
    public CargoTypeJson Type { get; set; }
    public required DriverResponseJson Driver { get; set; }
    public required VehicleResponseJson Vehicle { get; set; }
    public DateOnly DeliveryDate { get; set; }
    public IEnumerable<Guid> OrderIds { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}
