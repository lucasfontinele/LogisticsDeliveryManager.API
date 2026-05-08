using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Application.UseCases.Employees.CreateEmployee;

public class CreateEmployeeCommand
{
    public string Name { get; private set; }
    public string Document { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public RoleType RoleType { get; private set; }

    public CreateEmployeeCommand(string name, string document, string phoneNumber, string email, RoleType roleType)
    {
        Name = name;
        Document = document;
        PhoneNumber = phoneNumber;
        Email = email;
        RoleType = roleType;
    }
}
