using LogisticsDeliveryManager.Domain.Fleet.Drivers;
using LogisticsDeliveryManager.Domain.Fleet.Shared;
using LogisticsDeliveryManager.Domain.Fleet.Vehicles;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Fleet;

public class FleetAssignmentService
{
    public void AssignToRoute(
        Guid routeId,
        Vehicle? vehicle,
        Driver? driver,
        CargoRequirement? cargoRequirement,
        TimeSpan plannedDrivingTime)
    {
        var errors = new List<string>();

        if (routeId == Guid.Empty)
            errors.Add("Route id cannot be empty.");

        if (vehicle is null)
            errors.Add("Vehicle cannot be null.");

        if (driver is null)
            errors.Add("Driver cannot be null.");

        if (cargoRequirement is null)
            errors.Add("Cargo requirement cannot be null.");

        if (plannedDrivingTime <= TimeSpan.Zero)
            errors.Add("Planned driving time must be greater than zero.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);

        var validatedVehicle = vehicle!;
        var validatedDriver = driver!;
        var validatedCargoRequirement = cargoRequirement!;

        if (!validatedDriver.HasLicenseFor(validatedVehicle.RequiredLicenseCategory))
            errors.Add("Driver is not licensed for the vehicle.");

        if (!validatedDriver.CanDriveFor(plannedDrivingTime))
            errors.Add("Driver cannot exceed the daily work limit.");

        if (!validatedVehicle.CanCarry(validatedCargoRequirement))
            errors.Add("Vehicle cannot carry the informed cargo.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);

        validatedVehicle.AssignToRoute(routeId, validatedCargoRequirement);
        validatedDriver.AssignToRoute(routeId, plannedDrivingTime);
    }
}
