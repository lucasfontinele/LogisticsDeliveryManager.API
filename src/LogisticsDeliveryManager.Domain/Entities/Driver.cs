using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Driver
{
    public IEnumerable<DriverLicenseType> LicenseTypes { get; set; }
    public Employee Employee { get; set; }
}
