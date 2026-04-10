using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Customer : Person
{
    public CustomerType CustomerType { get; private set; }
    public List<Address> Addresses { get; private set; }

    private Customer() { }

    public Customer(string name, string document, string phoneNumber, Email email, CustomerType customerType, List<Address> addresses)
        : base(name, document, phoneNumber, email)
    {
        CustomerType = customerType;
        Addresses = addresses;
    }
}
