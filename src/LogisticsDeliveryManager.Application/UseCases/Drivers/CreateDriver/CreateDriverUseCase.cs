using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Drivers;
using LogisticsDeliveryManager.Domain.Repositories.Employees;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Drivers.CreateDriver;

public class CreateDriverUseCase : ICreateDriverUseCase
{
    private readonly IDriverRepository _driverRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDriverUseCase(
        IDriverRepository driverRepository,
        IEmployeeRepository employeeRepository,
        IUnitOfWork unitOfWork)
    {
        _driverRepository = driverRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Driver> Execute(CreateDriverCommand command)
    {
        var employee = await _employeeRepository.GetById(command.EmployeeId);
        if (employee is null)
        {
            throw new ErrorOnValidationException(["Employee not found."]);
        }

        var alreadyDriver = await _driverRepository.ExistDriverForEmployee(command.EmployeeId);
        if (alreadyDriver)
        {
            throw new ErrorOnValidationException(["This employee is already registered as a driver."]);
        }

        var driver = new Driver(employee, command.LicenseTypes);

        await _driverRepository.Add(driver);
        await _unitOfWork.Commit();

        return driver;
    }
}
