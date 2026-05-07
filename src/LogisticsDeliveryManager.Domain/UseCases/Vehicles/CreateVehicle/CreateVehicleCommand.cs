using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.UseCases.Vehicles.CreateVehicle;

public sealed record CreateVehicleCommand(
    string LicensePlate,
    string Model,
    double WeightCapacity,
    double VolumeCapacity,
    CompartmentType CompartmentType);
