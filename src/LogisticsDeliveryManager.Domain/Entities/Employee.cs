using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.ValueObjects;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Employee : Person
{
    public RoleType RoleType { get; set; }
}