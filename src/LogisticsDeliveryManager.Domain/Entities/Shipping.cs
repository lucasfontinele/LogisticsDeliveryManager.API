using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Shipping
{
    public long Id { get; private set; }
    public Order Order { get; private set; }
    public string Address { get; private set; }
    public DateOnly EstimatedDeliveryDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Shipping() { }

    public Shipping(Order order, string address, DateOnly estimatedDeliveryDate)
    {
        Validate(order, address, estimatedDeliveryDate);
        
        Order = order;
        Address = address;
        EstimatedDeliveryDate = estimatedDeliveryDate;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void Validate(Order order, string address, DateOnly estimatedDeliveryDate)
    {
        var errors = new List<string>();

        if (order == null)
            errors.Add("Order cannot be null.");

        if (string.IsNullOrWhiteSpace(address))
            errors.Add("Address cannot be empty.");

        if (estimatedDeliveryDate < DateOnly.FromDateTime(DateTime.UtcNow))
            errors.Add("Estimated delivery date cannot be in the past.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}