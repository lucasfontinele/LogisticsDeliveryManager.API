using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Vehicle : EntityBase
{
    public LicensePlate LicensePlate { get; private set; } = null!;
    public string Model { get; private set; } = string.Empty;
    public Weight WeightCapacity { get; private set; } = null!;
    public Volume VolumeCapacity { get; private set; } = null!;
    public CompartmentType CompartmentType { get; private set; }
    public bool IsReadyForOrders { get; private set; } = true;
    public bool IsFullyLoaded { get; private set; } = false;
    public Guid? CurrentDriverId { get; private set; }
    public Employee? CurrentDriver { get; private set; }

    public void AllocateDriver(Employee driver) 
    {
        CurrentDriver = driver;
        CurrentDriverId = driver.Id;
    }

    public void RemoveDriver() 
    {
        CurrentDriver = null;
        CurrentDriverId = null;
    }

    public void SetReadyForOrders(bool ready) => IsReadyForOrders = ready;
    public void SetFullyLoaded(bool fullyLoaded) => IsFullyLoaded = fullyLoaded;

    private Vehicle() { }

    public static Vehicle Register(
        LicensePlate licensePlate,
        string model,
        Weight weightCapacity,
        Volume volumeCapacity,
        CompartmentType compartmentType)
    {
        return new Vehicle(
            licensePlate,
            model,
            weightCapacity,
            volumeCapacity,
            compartmentType);
    }

    public Vehicle(LicensePlate licensePlate, string model, Weight weightCapacity, Volume volumeCapacity, CompartmentType compartmentType)
    {
        Validate(licensePlate, model, weightCapacity, volumeCapacity, compartmentType);

        LicensePlate = licensePlate;
        Model = model;
        WeightCapacity = weightCapacity;
        VolumeCapacity = volumeCapacity;
        CompartmentType = compartmentType;
    }

    private static void Validate(
        LicensePlate licensePlate,
        string model,
        Weight weightCapacity,
        Volume volumeCapacity,
        CompartmentType compartmentType)
    {
        var errors = new List<string>();

        if (licensePlate == null)
            errors.Add("License plate is required.");

        if (string.IsNullOrWhiteSpace(model))
            errors.Add("Model cannot be empty.");

        if (weightCapacity == null)
            errors.Add("Weight capacity is required.");

        if (volumeCapacity == null)
            errors.Add("Volume capacity is required.");

        if (!Enum.IsDefined(typeof(CompartmentType), compartmentType))
            errors.Add("Invalid compartment type.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
   
