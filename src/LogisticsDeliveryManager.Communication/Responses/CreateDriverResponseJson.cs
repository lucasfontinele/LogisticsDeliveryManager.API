namespace LogisticsDeliveryManager.Communication.Responses;

public class CreateDriverResponseJson
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public IEnumerable<int> LicenseTypes { get; set; } = [];
}
