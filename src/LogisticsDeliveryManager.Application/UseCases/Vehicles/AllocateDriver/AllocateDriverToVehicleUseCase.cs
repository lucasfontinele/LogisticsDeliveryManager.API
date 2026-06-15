using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Employees;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Vehicles.AllocateDriver;

public class AllocateDriverToVehicleUseCase : IAllocateDriverToVehicleUseCase
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IEmployeeRepository _driverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AllocateDriverToVehicleUseCase(IVehicleRepository vehicleRepository, IEmployeeRepository driverRepository, IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _driverRepository = driverRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(AllocateDriverToVehicleCommand command)
    {
        var vehicle = await _vehicleRepository.GetById(command.VehicleId);
        if (vehicle is null)
            throw new ErrorOnValidationException(["Vehicle not found."]);

        var driver = await _driverRepository.GetById(command.DriverId);
        if (driver is null || driver.RoleType != Domain.Enums.RoleType.Driver)
            throw new ErrorOnValidationException(["Driver not found."]);

        vehicle.AllocateDriver(driver);

        _vehicleRepository.Update(vehicle);
        await _unitOfWork.Commit();
    }
}
