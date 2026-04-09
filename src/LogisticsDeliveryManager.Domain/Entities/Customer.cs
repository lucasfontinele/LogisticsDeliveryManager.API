using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Entities.Base;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Customer : Person
{
    public CustomerType CustomerType { get; set; }
    public List<Address> Addresses { get; set; }
}
