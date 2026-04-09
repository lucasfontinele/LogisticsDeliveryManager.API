using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Address
{
    public long Id { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public PostalCode PostalCode { get; private set; }

    private Address() { }

    public Address(string street, string city, string state, PostalCode postalCode)
    {
        Validate(street, city, state, postalCode);
        
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }

    static void Validate(string street, string city, string state, PostalCode postalCode)
    {
        if (string.IsNullOrEmpty(street))
            throw new ErrorOnValidationException(new List<string> { "Street is required." });
        if (string.IsNullOrEmpty(city))
            throw new ErrorOnValidationException(new List<string> { "City is required." });
        if (string.IsNullOrEmpty(state))
            throw new ErrorOnValidationException(new List<string> { "State is required." });
        if (postalCode == null)
            throw new ErrorOnValidationException(new List<string> { "Postal code is required." });
    }
}