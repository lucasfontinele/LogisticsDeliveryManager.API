namespace LogisticsDeliveryManager.Communication.Responses;

public class EmployeeResponseJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int RoleType { get; set; }
    public IEnumerable<int> LicenseTypes { get; set; } = [];
}
