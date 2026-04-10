using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Domain.ValueObjects;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Employee : Person
{
    public RoleType RoleType { get; private set; }

    private Employee() { }

    public Employee(string name, string document, string phoneNumber, Email email, RoleType roleType)
        : base(name, document, phoneNumber, email)
    {
        RoleType = roleType;
    }
}