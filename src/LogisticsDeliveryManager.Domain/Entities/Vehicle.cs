using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Vehicle
{
    public long Id { get; set; }
    public string LicensePlate { get; set; }
    public string Model { get; set; }
    public double WeightCapacity { get; set; }
    public double VolumeCapacity { get; set; }
    public CompartmentType CompartmentType { get; set; }
}
   
