using LogisticsDeliveryManager.Domain.Fleet;
using LogisticsDeliveryManager.Domain.Fleet.Drivers;
using LogisticsDeliveryManager.Domain.Fleet.Shared;
using LogisticsDeliveryManager.Domain.Fleet.Vehicles;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Fleet.AssignVehicleToRoute;

public sealed class AssignVehicleToRouteHandler
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly FleetAssignmentService _fleetAssignmentService;
    private readonly IUnitOfWork _unitOfWork;

    public AssignVehicleToRouteHandler(
        IVehicleRepository vehicleRepository,
        IDriverRepository driverRepository,
        FleetAssignmentService fleetAssignmentService,
        IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _driverRepository = driverRepository;
        _fleetAssignmentService = fleetAssignmentService;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(AssignVehicleToRouteCommand command, CancellationToken cancellationToken)
    {
        Validate(command);

        var vehicle = await _vehicleRepository.GetByIdAsync(command.VehicleId, cancellationToken);

        if (vehicle is null)
        {
            throw new NotFoundException("Vehicle not found.");
        }

        var driver = await _driverRepository.GetByIdAsync(command.DriverId, cancellationToken);

        if (driver is null)
        {
            throw new NotFoundException("Driver not found.");
        }

        var cargoRequirement = new CargoRequirement(
            command.CargoType,
            command.Weight,
            command.Volume,
            command.RequiresRefrigeration,
            command.IsFragile,
            command.IsDangerous);

        _fleetAssignmentService.AssignToRoute(
            command.RouteId,
            vehicle,
            driver,
            cargoRequirement,
            command.PlannedDrivingTime);

        await _vehicleRepository.UpdateAsync(vehicle, cancellationToken);
        await _driverRepository.UpdateAsync(driver, cancellationToken);
        await _unitOfWork.Commit();
    }

    private static void Validate(AssignVehicleToRouteCommand? command)
    {
        if (command is null)
            throw new ErrorOnValidationException(["Command cannot be null."]);
    }
}
