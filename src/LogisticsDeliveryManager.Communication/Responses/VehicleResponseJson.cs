namespace LogisticsDeliveryManager.Communication.Responses;

public class VehicleResponseJson
{
    public long Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public double WeightCapacity { get; set; }
    public double VolumeCapacity { get; set; }
    public int CompartmentType { get; set; }
    public long? CurrentDriverId { get; set; }
}
