using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Customer
{
    public IEnumerable<CustomerType> CustomerType { get; set; }
    public List<Address> Addresses { get; set; }
}
