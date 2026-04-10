using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Batch
{
    public long Id { get; set; }
    public CargoType Type { get; set; }
    public Driver Driver { get; set; }
    public Vehicle Vehicle { get; set; }
    public DateOnly DeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }

    private Batch() { }

    public Batch(CargoType type, Driver driver, Vehicle vehicle, DateOnly deliveryDate)
    {
        Validate(type, driver, vehicle, deliveryDate);
        
        Type = type;
        Driver = driver;
        Vehicle = vehicle;
        DeliveryDate = deliveryDate;
        CreatedAt = DateTime.UtcNow;
    }

    private static void Validate(CargoType type, Driver driver, Vehicle vehicle, DateOnly deliveryDate)
    {
        var errors = new List<string>();

         if (!Enum.IsDefined(typeof(CargoType), type))
            errors.Add("Invalid cargo type.");

        if (driver == null)
            errors.Add("Driver cannot be null.");

        if (vehicle == null)
            errors.Add("Vehicle cannot be null.");

        if (deliveryDate < DateOnly.FromDateTime(DateTime.UtcNow))
            errors.Add("Delivery date cannot be in the past.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
