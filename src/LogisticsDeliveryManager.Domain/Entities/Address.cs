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
        var errors = new List<string>();

        if (string.IsNullOrEmpty(street))
            errors.Add("Street is required.");
            
        if (string.IsNullOrEmpty(city))
            errors.Add("City is required.");

        if (string.IsNullOrEmpty(state))
            errors.Add("State is required.");

        if (postalCode == null)
            errors.Add("Postal code is required.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}