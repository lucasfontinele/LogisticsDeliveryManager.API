using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsDeliveryManager.Domain.Entities;

[ComplexType]
public sealed record Address
{
    public required string Street { get; init; } = string.Empty;
    public required string City { get; init; } = string.Empty;
    public required string State { get; init; } = string.Empty;
    public required PostalCode PostalCode { get; init; } = null!;

    [SetsRequiredMembers]
    private Address() { }

    [SetsRequiredMembers]
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
