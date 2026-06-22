using LogisticsDeliveryManager.Exception.ExceptionsBase;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace LogisticsDeliveryManager.Domain.ValueObjects;

[ComplexType]
public sealed record LicensePlate
{
    public string Value { get; init; } = string.Empty;

    private LicensePlate() { }

    public LicensePlate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ErrorOnValidationException(new List<string> { "License plate cannot be empty." });

        var normalized = value.Trim().ToUpperInvariant();

        if (!Regex.IsMatch(normalized, "^[A-Z0-9-]{1,10}$"))
            throw new ErrorOnValidationException(new List<string> { "License plate format is invalid." });

        Value = normalized;
    }

    public static implicit operator string(LicensePlate licensePlate) => licensePlate.Value;
    public static implicit operator LicensePlate(string value) => new(value);

    public override string ToString() => Value;
}
