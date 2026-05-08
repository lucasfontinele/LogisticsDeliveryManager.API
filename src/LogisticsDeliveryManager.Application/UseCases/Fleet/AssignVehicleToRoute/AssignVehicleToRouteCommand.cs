using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Application.UseCases.Fleet.AssignVehicleToRoute;

public sealed record AssignVehicleToRouteCommand(
    Guid RouteId,
    long VehicleId,
    long DriverId,
    CargoType CargoType,
    double Weight,
    double Volume,
    bool RequiresRefrigeration,
    bool IsFragile,
    bool IsDangerous,
    TimeSpan PlannedDrivingTime);
