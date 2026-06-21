using LogisticsDeliveryManager.Exception.ExceptionsBase;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace LogisticsDeliveryManager.Domain.ValueObjects;

[ComplexType]
public sealed record Volume
{
    public double Value { get; init; }

    private Volume() { }

    public Volume(double value)
    {
        if (value <= 0)
            throw new ErrorOnValidationException(new List<string> { "Volume must be greater than zero." });

        Value = value;
    }

    public static implicit operator double(Volume volume) => volume.Value;
    public static implicit operator Volume(double value) => new(value);

    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}
