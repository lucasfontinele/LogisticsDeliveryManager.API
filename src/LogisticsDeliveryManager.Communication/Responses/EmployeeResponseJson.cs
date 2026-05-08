namespace LogisticsDeliveryManager.Communication.Responses;

public class EmployeeResponseJson
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int RoleType { get; set; }
}
