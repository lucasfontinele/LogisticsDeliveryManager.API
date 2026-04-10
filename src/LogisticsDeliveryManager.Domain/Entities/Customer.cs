using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Customer : Person
{
    private readonly List<Address> _addresses = new();

    public CustomerType CustomerType { get; private set; }

    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();

    private Customer() { }

    public Customer(
        string name,
        string document,
        string phoneNumber,
        Email email,
        CustomerType customerType,
        IEnumerable<Address> addresses)
        : base(name, document, phoneNumber, email)
    {
        SetCustomerType(customerType);
        AddAddresses(addresses);
    }

    private void SetCustomerType(CustomerType customerType)
    {   

        if (!Enum.IsDefined(typeof(CustomerType), customerType))
            throw new ErrorOnValidationException(["Invalid customer type."]);
            
        CustomerType = customerType;
    }

    public void AddAddress(Address address)
    {
        if (address is null)
            throw new ErrorOnValidationException(["Address cannot be null"]);

        _addresses.Add(address);
    }

    public void AddAddresses(IEnumerable<Address> addresses)
    {
        if (addresses is null)
            throw new ErrorOnValidationException(["Addresses cannot be null"]);

        foreach (var address in addresses)
        {
            AddAddress(address);
        }
    }

    public void RemoveAddress(Address address)
    {
        if (address is null)
            throw new ErrorOnValidationException(["Address cannot be null"]);

        _addresses.Remove(address);
    }
}