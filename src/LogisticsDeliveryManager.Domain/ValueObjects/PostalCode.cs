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
        if (!Validate(code))
            throw new ErrorOnValidationException(new List<string> { "Postal code format is invalid." });
        Code = code;
    }

    private static bool Validate(string postalCode)
    {
        var regex = new Regex(@"^\d{5}-?\d{3}$")
        return regex.IsMatch(postalCode);
    }

    public override string ToString() => Code;
        
    public static implicit operator string(PostalCode postalCode) => postalCode.Code;
    public static implicit operator PostalCode(string postalCode) => new(postalCode);
}