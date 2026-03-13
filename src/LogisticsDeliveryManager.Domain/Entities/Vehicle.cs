using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities
{
    public class Vehicle
    {
        public long Id { get; set; }
        public String LicensePlate { get; set; }
        public String Model { get; set; }
        public double WeightCapacity { get; set; }
        public double VolumeCapacity { get; set; }
        public DriverLicenseType LicenseType { get; set; }
    }
}
