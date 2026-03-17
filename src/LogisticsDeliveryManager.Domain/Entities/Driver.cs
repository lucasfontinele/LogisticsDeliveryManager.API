using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities
{
    public class Driver 
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Document { get; set; }
        public List<DriverLicenseType> LicenseType { get; set; }
    }
}
