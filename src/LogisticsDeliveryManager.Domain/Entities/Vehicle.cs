using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Vehicle
{
    public long Id { get; private set; }
    public string LicensePlate { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public double WeightCapacity { get; private set; }
    public double VolumeCapacity { get; private set; }
    public CompartmentType CompartmentType { get; private set; }

    private Vehicle() { }

    public Vehicle(string licensePlate, string model, double weightCapacity, double volumeCapacity, CompartmentType compartmentType)
    {
        Validate(licensePlate, model, weightCapacity, volumeCapacity);

        LicensePlate = licensePlate;
        Model = model;
        WeightCapacity = weightCapacity;
        VolumeCapacity = volumeCapacity;
        CompartmentType = compartmentType;
    }

    private static void Validate(string licensePlate, string model, double weightCapacity, double volumeCapacity)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(licensePlate))
            errors.Add("License plate cannot be empty.");

        if (string.IsNullOrWhiteSpace(model))
            errors.Add("Model cannot be empty.");

        if (weightCapacity <= 0)
            errors.Add("Weight capacity must be greater than zero.");

        if (volumeCapacity <= 0)
            errors.Add("Volume capacity must be greater than zero.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
   
