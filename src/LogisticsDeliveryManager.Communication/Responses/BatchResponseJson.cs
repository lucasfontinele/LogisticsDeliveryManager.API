using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Responses;

public class BatchResponseJson
{
    public Guid Id { get; set; }
    public CargoTypeJson Type { get; set; }
    public DriverResponseJson Driver { get; set; } = null!;
    public VehicleResponseJson Vehicle { get; set; } = null!;
    public DateOnly DeliveryDate { get; set; }
    public IEnumerable<Guid> OrderIds { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}
