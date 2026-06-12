using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Batches;
using LogisticsDeliveryManager.Domain.Repositories.Employees;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using DomainBatch = LogisticsDeliveryManager.Domain.Entities.Batch;

namespace LogisticsDeliveryManager.Application.UseCases.Batch.CreateBatch;

public sealed class CreateBatchUseCase : ICreateBatchUseCase
{
    private readonly IBatchRepository _batchRepository;
    private readonly IEmployeeRepository _driverRepository;
    private readonly IVehicleRepository _vehicle_repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBatchUseCase(
        IBatchRepository batchRepository,
        IEmployeeRepository driverRepository,
        IVehicleRepository vehicleRepository,
        IUnitOfWork unitOfWork)
    {
        _batchRepository = batchRepository;
        _driverRepository = driverRepository;
        _vehicle_repository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DomainBatch> Execute(CreateBatchCommand command)
    {
        var driver = await _driverRepository.GetById(command.DriverId);
        if (driver is null || driver.RoleType != Domain.Enums.RoleType.Driver)
            throw new ErrorOnValidationException(["Driver not found."]);

        var vehicle = await _vehicle_repository.GetById(command.VehicleId);
        if (vehicle is null)
            throw new ErrorOnValidationException(["Vehicle not found."]);

        var batch = new DomainBatch(
            command.Type,
            driver.Id,
            vehicle.Id,
            vehicle.WeightCapacity,
            vehicle.VolumeCapacity,
            command.DeliveryDate);

        await _batchRepository.Add(batch);
        await _unitOfWork.Commit();

        return batch;
    }
}
