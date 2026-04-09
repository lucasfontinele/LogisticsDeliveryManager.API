using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Entities.Base;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Employee : Person
{
    public RoleType RoleType { get; private set; }

    private Employee() { }

    public Employee(string name, string document, string phoneNumber, Email email, RoleType roleType)
        : base(name, document, phoneNumber, email)
    {
        Validate(roleType);

        RoleType = roleType;
    }

    private static void Validate(RoleType roleType)
    {
        var errors = new List<string>();

        if (roleType == RoleType.None)
            errors.Add("Role type cannot be empty.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}