using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Batches;
using LogisticsDeliveryManager.Domain.Repositories.Drivers;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using DomainBatch = LogisticsDeliveryManager.Domain.Entities.Batch;

namespace LogisticsDeliveryManager.Application.UseCases.Batch.CreateBatch;

public sealed class CreateBatchUseCase : ICreateBatchUseCase
{
    private readonly IBatchRepository _batchRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBatchUseCase(
        IBatchRepository batchRepository,
        IDriverRepository driverRepository,
        IVehicleRepository vehicleRepository,
        IUnitOfWork unitOfWork)
    {
        _batchRepository = batchRepository;
        _driverRepository = driverRepository;
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DomainBatch> Execute(CreateBatchCommand command)
    {
        var driver = await _driverRepository.GetById(command.DriverId);
        if (driver is null)
            throw new ErrorOnValidationException(["Driver not found."]);

        var vehicle = await _vehicleRepository.GetById(command.VehicleId);
        if (vehicle is null)
            throw new ErrorOnValidationException(["Vehicle not found."]);

        var batch = new DomainBatch(
            command.Type,
            driver,
            vehicle,
            command.DeliveryDate);

        await _batchRepository.Add(batch);
        await _unitOfWork.Commit();

        return batch;
    }
}
