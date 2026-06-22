namespace LogisticsDeliveryManager.Communication.Requests;

public class RegisterEmployeeAsDriverRequestJson
{
    public IEnumerable<int> LicenseTypes { get; set; } = [];
}
