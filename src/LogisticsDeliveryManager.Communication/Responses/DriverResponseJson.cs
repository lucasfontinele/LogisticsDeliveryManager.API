namespace LogisticsDeliveryManager.Communication.Responses;

public class DriverResponseJson
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public IEnumerable<int> LicenseTypes { get; set; } = [];
}
