using LogisticsDeliveryManager.Exception.ExceptionsBase;
using System.Text.RegularExpressions;

namespace LogisticsDeliveryManager.Domain.ValueObjects;

public record PostalCode 
{
    public string Code { get; }

    public PostalCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ErrorOnValidationException(new List<string> { "Postal code cannot be empty." });
        Code = code;
    }

      private static bool Validate(string postalCode)
    {
        return Regex.IsMatch(postalCode, @"^\d{5}-?\d{3}$");
    }
}