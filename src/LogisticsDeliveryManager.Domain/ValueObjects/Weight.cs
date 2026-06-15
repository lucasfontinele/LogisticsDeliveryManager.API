using LogisticsDeliveryManager.Exception.ExceptionsBase;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace LogisticsDeliveryManager.Domain.ValueObjects;

[ComplexType]
public sealed record Weight
{
    public double Value { get; init; }

    protected Weight() { }

    public Weight(double value)
    {
        if (value <= 0)
            throw new ErrorOnValidationException(new List<string> { "Weight must be greater than zero." });

        Value = value;
    }

    public static implicit operator double(Weight weight) => weight.Value;
    public static implicit operator Weight(double value) => new(value);

    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}
