using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Fleet.Shared;

public sealed record CargoRequirement
{
    public CargoType CargoType { get; }
    public double Weight { get; }
    public double Volume { get; }
    public bool RequiresRefrigeration { get; }
    public bool IsFragile { get; }
    public bool IsDangerous { get; }

    public bool RequiresRigidDeliveryWindow => CargoType == CargoType.Medicine;
    public bool NeedsRefrigeratedCompartment =>
        RequiresRefrigeration || CargoType == CargoType.Refrigerated || CargoType == CargoType.Medicine;

    public CargoRequirement(
        CargoType cargoType,
        double weight,
        double volume,
        bool requiresRefrigeration,
        bool isFragile,
        bool isDangerous)
    {
        Validate(cargoType, weight, volume);

        CargoType = cargoType;
        Weight = weight;
        Volume = volume;
        RequiresRefrigeration = requiresRefrigeration;
        IsFragile = isFragile;
        IsDangerous = isDangerous;
    }

    public bool CanBeTransportedIn(CompartmentType compartmentType)
    {
        if (NeedsRefrigeratedCompartment && compartmentType != CompartmentType.RefrigeratedBody)
            return false;

        if ((IsDangerous || CargoType == CargoType.Dangerous) && compartmentType == CompartmentType.RefrigeratedBody)
            return false;

        return true;
    }

    public bool CanShareVehicleWith(CargoRequirement other)
    {
        if (other is null)
            throw new ErrorOnValidationException(["Cargo requirement cannot be null."]);

        if (IsDangerous || CargoType == CargoType.Dangerous)
            return false;

        if (other.IsDangerous || other.CargoType == CargoType.Dangerous)
            return false;

        if (NeedsRefrigeratedCompartment != other.NeedsRefrigeratedCompartment)
            return false;

        return true;
    }

    private static void Validate(CargoType cargoType, double weight, double volume)
    {
        var errors = new List<string>();

        if (!Enum.IsDefined(typeof(CargoType), cargoType))
            errors.Add("Invalid cargo type.");

        if (weight <= 0)
            errors.Add("Cargo weight must be greater than zero.");

        if (volume <= 0)
            errors.Add("Cargo volume must be greater than zero.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
