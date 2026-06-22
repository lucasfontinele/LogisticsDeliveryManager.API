using LogisticsDeliveryManager.Exception.ExceptionsBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsDeliveryManager.Domain.ValueObjects;

[ComplexType]
public sealed record DeliveryWindow
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    private DeliveryWindow() { }

    public DeliveryWindow(DateTime start, DateTime end)
    {
        if (start == default)
            throw new ErrorOnValidationException(new List<string> { "Delivery window start is required." });

        if (end == default)
            throw new ErrorOnValidationException(new List<string> { "Delivery window end is required." });

        if (start >= end)
            throw new ErrorOnValidationException(new List<string> { "Delivery window start must be before delivery window end." });

        Start = start;
        End = end;
    }

    public override string ToString() => $"{Start:o} - {End:o}";
}
