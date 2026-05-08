namespace LogisticsDeliveryManager.Communication.Responses;

public class DriverResponseJson
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public IEnumerable<int> LicenseTypes { get; set; } = [];
}
