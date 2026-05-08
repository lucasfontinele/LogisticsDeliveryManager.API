namespace LogisticsDeliveryManager.Communication.Responses;

public class CreateDriverResponseJson
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public IEnumerable<int> LicenseTypes { get; set; } = [];
}
