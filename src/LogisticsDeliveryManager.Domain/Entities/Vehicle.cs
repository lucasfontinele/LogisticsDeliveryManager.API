using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities
{
    public class Vehicle
    {
        public long Id { get; set; }
        public String LicensePlate { get; set; }
        public String Model { get; set; }
        public double Capacity { get; set; }
        public DriverLicenseType LicenseType { get; set; }
    }
}
