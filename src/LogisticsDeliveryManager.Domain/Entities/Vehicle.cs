using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

using LogisticsDeliveryManager.Domain.Entities.Base;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Vehicle : EntityBase
{
    public string LicensePlate { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public double WeightCapacity { get; private set; }
    public double VolumeCapacity { get; private set; }
    public CompartmentType CompartmentType { get; private set; }
    public bool IsReadyForOrders { get; private set; } = true;
    public bool IsFullyLoaded { get; private set; } = false;
    public Driver? CurrentDriver { get; private set; }

    public void AllocateDriver(Driver driver) => CurrentDriver = driver;
    public void SetReadyForOrders(bool ready) => IsReadyForOrders = ready;
    public void SetFullyLoaded(bool fullyLoaded) => IsFullyLoaded = fullyLoaded;

    private Vehicle() { }

    public static Vehicle Register(
        string licensePlate,
        string model,
        double weightCapacity,
        double volumeCapacity,
        CompartmentType compartmentType)
    {
        return new Vehicle(
            licensePlate,
            model,
            weightCapacity,
            volumeCapacity,
            compartmentType);
    }

    public Vehicle(string licensePlate, string model, double weightCapacity, double volumeCapacity, CompartmentType compartmentType)
    {
        Validate(licensePlate, model, weightCapacity, volumeCapacity, compartmentType);

        LicensePlate = licensePlate;
        Model = model;
        WeightCapacity = weightCapacity;
        VolumeCapacity = volumeCapacity;
        CompartmentType = compartmentType;
    }

    private static void Validate(
        string licensePlate,
        string model,
        double weightCapacity,
        double volumeCapacity,
        CompartmentType compartmentType)
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

        if (!Enum.IsDefined(typeof(CompartmentType), compartmentType))
            errors.Add("Invalid compartment type.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
   
