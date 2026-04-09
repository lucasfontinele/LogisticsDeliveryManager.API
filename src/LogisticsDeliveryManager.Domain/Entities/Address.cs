using LogisticsDeliveryManager.Domain.ValueObjects;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Address
{
    public long Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public PostalCode PostalCode { get; set; }
}