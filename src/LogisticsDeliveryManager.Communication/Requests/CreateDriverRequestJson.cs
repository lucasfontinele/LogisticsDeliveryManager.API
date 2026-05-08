namespace LogisticsDeliveryManager.Communication.Requests;

public class CreateDriverRequestJson
{
    public Guid EmployeeId { get; set; }
    public IEnumerable<int> LicenseTypes { get; set; } = [];
}
