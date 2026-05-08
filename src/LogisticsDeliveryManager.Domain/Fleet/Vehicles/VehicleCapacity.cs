using LogisticsDeliveryManager.Domain.Fleet.Shared;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Fleet.Vehicles;

public sealed record VehicleCapacity
{
    public double MaximumWeight { get; }
    public double MaximumVolume { get; }

    public VehicleCapacity(double maximumWeight, double maximumVolume)
    {
        var errors = new List<string>();

        if (maximumWeight <= 0)
            errors.Add("Maximum weight must be greater than zero.");

        if (maximumVolume <= 0)
            errors.Add("Maximum volume must be greater than zero.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);

        MaximumWeight = maximumWeight;
        MaximumVolume = maximumVolume;
    }

    public bool CanAccommodate(double currentWeight, double currentVolume, CargoRequirement cargoRequirement)
    {
        if (cargoRequirement is null)
            throw new ErrorOnValidationException(["Cargo requirement cannot be null."]);

        if (currentWeight < 0 || currentVolume < 0)
            throw new ErrorOnValidationException(["Current load cannot be negative."]);

        return currentWeight + cargoRequirement.Weight <= MaximumWeight
            && currentVolume + cargoRequirement.Volume <= MaximumVolume;
    }
}
