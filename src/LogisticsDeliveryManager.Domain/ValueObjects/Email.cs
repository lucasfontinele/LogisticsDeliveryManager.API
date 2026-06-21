using LogisticsDeliveryManager.Exception.ExceptionsBase;
using System.Text.RegularExpressions;

using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsDeliveryManager.Domain.ValueObjects;

[ComplexType]
public record Email
{
    public string Address { get; init; } = string.Empty;

    protected Email() { }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ErrorOnValidationException(new List<string> { "E-mail cannot be empty." });

        if (!Validate(address))
            throw new ErrorOnValidationException(new List<string> { "Invalid e-mail format." });

        Address = address;
    }

    private static bool Validate(string email)
    {
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return regex.IsMatch(email);
    }

    public override string ToString() => Address;

    public static implicit operator string(Email email) => email.Address;
    public static implicit operator Email(string address) => new(address);
}
