using LogisticsDeliveryManager.Domain.Fleet.Drivers;
using LogisticsDeliveryManager.Domain.Fleet.Shared;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Fleet.Vehicles;

public class Vehicle
{
    private readonly List<CargoRequirement> _assignedCargoRequirements = new();

    public long Id { get; private set; }
    public LicensePlate LicensePlate { get; private set; }
    public string Model { get; private set; }
    public VehicleCapacity Capacity { get; private set; }
    public CompartmentType CompartmentType { get; private set; }
    public VehicleStatus Status { get; private set; }
    public DateTime? MaintenanceScheduledFor { get; private set; }
    public DateTime? BreakdownRegisteredAt { get; private set; }
    public Guid? CurrentRouteId { get; private set; }
    public IReadOnlyCollection<CargoRequirement> AssignedCargoRequirements => _assignedCargoRequirements.AsReadOnly();
    public double CurrentWeightLoad => _assignedCargoRequirements.Sum(requirement => requirement.Weight);
    public double CurrentVolumeLoad => _assignedCargoRequirements.Sum(requirement => requirement.Volume);
    public DriverLicenseCategory RequiredLicenseCategory => DetermineRequiredLicenseCategory();

    private Vehicle()
    {
        LicensePlate = null!;
        Model = string.Empty;
        Capacity = null!;
    }

    public Vehicle(
        LicensePlate licensePlate,
        string model,
        VehicleCapacity capacity,
        CompartmentType compartmentType)
    {
        Validate(licensePlate, model, capacity, compartmentType);

        LicensePlate = licensePlate;
        Model = model.Trim();
        Capacity = capacity;
        CompartmentType = compartmentType;
        Status = VehicleStatus.Available;
    }

    public bool CanCarry(CargoRequirement cargoRequirement)
    {
        if (cargoRequirement is null)
            throw new ErrorOnValidationException(["Cargo requirement cannot be null."]);

        if (Status is VehicleStatus.UnderMaintenance or VehicleStatus.BrokenDown)
            return false;

        if (!cargoRequirement.CanBeTransportedIn(CompartmentType))
            return false;

        if (!Capacity.CanAccommodate(CurrentWeightLoad, CurrentVolumeLoad, cargoRequirement))
            return false;

        return _assignedCargoRequirements.All(existingCargo =>
            existingCargo.CanShareVehicleWith(cargoRequirement)
            && cargoRequirement.CanShareVehicleWith(existingCargo));
    }

    public void AssignToRoute(Guid routeId, CargoRequirement? cargoRequirement)
    {
        var errors = new List<string>();

        if (routeId == Guid.Empty)
            errors.Add("Route id cannot be empty.");

        if (cargoRequirement is null)
            errors.Add("Cargo requirement cannot be null.");

        if (Status == VehicleStatus.UnderMaintenance)
            errors.Add("Vehicle under maintenance cannot be assigned to a route.");

        if (Status == VehicleStatus.BrokenDown)
            errors.Add("Vehicle with breakdown cannot be assigned to a route.");

        if (CurrentRouteId.HasValue && CurrentRouteId.Value != routeId)
            errors.Add("Vehicle is already assigned to another route.");

        if (cargoRequirement is not null && !CanCarry(cargoRequirement))
            errors.Add("Vehicle cannot carry the informed cargo.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);

        var validatedCargoRequirement = cargoRequirement!;

        CurrentRouteId ??= routeId;
        _assignedCargoRequirements.Add(validatedCargoRequirement);
        Status = VehicleStatus.AssignedToRoute;
    }

    public void ReleaseFromRoute(Guid routeId)
    {
        if (!CurrentRouteId.HasValue || CurrentRouteId.Value != routeId)
            throw new ErrorOnValidationException(["Vehicle is not assigned to the informed route."]);

        CurrentRouteId = null;
        _assignedCargoRequirements.Clear();
        Status = BreakdownRegisteredAt.HasValue
            ? VehicleStatus.BrokenDown
            : MaintenanceScheduledFor.HasValue
                ? VehicleStatus.UnderMaintenance
                : VehicleStatus.Available;
    }

    public void ScheduleMaintenance(DateTime scheduledFor)
    {
        if (scheduledFor <= DateTime.UtcNow)
            throw new ErrorOnValidationException(["Maintenance date must be in the future."]);

        MaintenanceScheduledFor = scheduledFor;
    }

    public void StartMaintenance()
    {
        if (CurrentRouteId.HasValue)
            throw new ErrorOnValidationException(["Vehicle assigned to a route cannot start maintenance."]);

        Status = VehicleStatus.UnderMaintenance;
    }

    public void FinishMaintenance()
    {
        if (Status != VehicleStatus.UnderMaintenance)
            throw new ErrorOnValidationException(["Vehicle is not under maintenance."]);

        MaintenanceScheduledFor = null;
        Status = BreakdownRegisteredAt.HasValue ? VehicleStatus.BrokenDown : VehicleStatus.Available;
    }

    public void RegisterBreakdown()
    {
        BreakdownRegisteredAt = DateTime.UtcNow;
        Status = VehicleStatus.BrokenDown;
    }

    public void ResolveBreakdown()
    {
        if (!BreakdownRegisteredAt.HasValue)
            throw new ErrorOnValidationException(["Vehicle does not have a registered breakdown."]);

        BreakdownRegisteredAt = null;
        Status = MaintenanceScheduledFor.HasValue
            ? VehicleStatus.UnderMaintenance
            : CurrentRouteId.HasValue
                ? VehicleStatus.AssignedToRoute
                : VehicleStatus.Available;
    }

    private static void Validate(
        LicensePlate licensePlate,
        string model,
        VehicleCapacity capacity,
        CompartmentType compartmentType)
    {
        var errors = new List<string>();

        if (licensePlate is null)
            errors.Add("License plate cannot be null.");

        if (string.IsNullOrWhiteSpace(model))
            errors.Add("Model cannot be empty.");

        if (capacity is null)
            errors.Add("Capacity cannot be null.");

        if (!Enum.IsDefined(typeof(CompartmentType), compartmentType))
            errors.Add("Invalid compartment type.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }

    private DriverLicenseCategory DetermineRequiredLicenseCategory()
    {
        if (CompartmentType == CompartmentType.Tank || Capacity.MaximumWeight > 23000)
            return DriverLicenseCategory.E;

        if (Capacity.MaximumWeight > 12000)
            return DriverLicenseCategory.D;

        if (Capacity.MaximumWeight > 3500)
            return DriverLicenseCategory.C;

        return DriverLicenseCategory.B;
    }
}
