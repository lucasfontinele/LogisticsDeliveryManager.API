namespace LogisticsDeliveryManager.Communication.Requests;

public class CreateEmployeeRequestJson
{
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int RoleType { get; set; }
}
