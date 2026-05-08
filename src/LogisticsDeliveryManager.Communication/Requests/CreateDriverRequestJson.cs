namespace LogisticsDeliveryManager.Communication.Requests;

public class CreateDriverRequestJson
{
    public long EmployeeId { get; set; }
    public IEnumerable<int> LicenseTypes { get; set; } = [];
}
