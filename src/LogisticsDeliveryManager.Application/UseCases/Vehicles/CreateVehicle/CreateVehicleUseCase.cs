using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using LogisticsDeliveryManager.Domain.Services.Vehicles;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Vehicles.CreateVehicle;

public sealed class CreateVehicleUseCase : ICreateVehicleUseCase
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IVehicleDomainService _vehicleDomainService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IVehicleDomainService vehicleDomainService,
        IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _vehicleDomainService = vehicleDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Vehicle> Execute(CreateVehicleCommand command)
    {
        Validate(command);

        await _vehicleDomainService.ValidateUniqueLicensePlate(command.LicensePlate);

        var vehicle = Vehicle.Register(
            command.LicensePlate,
            command.Model,
            command.WeightCapacity,
            command.VolumeCapacity,
            command.CompartmentType);

        await _vehicleRepository.Add(vehicle);
        await _unitOfWork.Commit();

        return vehicle;
    }

    private static void Validate(CreateVehicleCommand? command)
    {
        if (command is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);
    }
}
