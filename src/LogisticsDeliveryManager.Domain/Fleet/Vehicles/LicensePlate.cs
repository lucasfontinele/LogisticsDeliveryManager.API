using LogisticsDeliveryManager.Exception.ExceptionsBase;
using System.Text.RegularExpressions;

namespace LogisticsDeliveryManager.Domain.Fleet.Vehicles;

public sealed record LicensePlate
{
    private static readonly Regex LegacyPattern = new("^[A-Z]{3}[0-9]{4}$", RegexOptions.Compiled);
    private static readonly Regex MercosulPattern = new("^[A-Z]{3}[0-9][A-Z][0-9]{2}$", RegexOptions.Compiled);

    public string Value { get; }

    public LicensePlate(string value)
    {
        var normalizedValue = Normalize(value);

        if (!IsValid(normalizedValue))
            throw new ErrorOnValidationException(["Invalid license plate."]);

        Value = normalizedValue;
    }

    public override string ToString() => Value;

    public static implicit operator string(LicensePlate licensePlate) => licensePlate.Value;
    public static implicit operator LicensePlate(string value) => new(value);

    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ErrorOnValidationException(["License plate cannot be empty."]);

        return value
            .Trim()
            .ToUpperInvariant()
            .Replace("-", string.Empty);
    }

    private static bool IsValid(string value)
    {
        return LegacyPattern.IsMatch(value) || MercosulPattern.IsMatch(value);
    }
}
