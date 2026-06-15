using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Employees;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Drivers.CreateDriver;

public class CreateDriverUseCase : ICreateDriverUseCase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDriverUseCase(
        IEmployeeRepository employeeRepository,
        IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Employee> Execute(CreateDriverCommand command)
    {
        var employee = await _employeeRepository.GetById(command.EmployeeId);
        if (employee is null)
            throw new ErrorOnValidationException(["Employee not found."]);

        if (employee.RoleType == Domain.Enums.RoleType.Driver)
            throw new ErrorOnValidationException(["This employee is already registered as a driver."]);

        employee.RegisterAsDriver(command.LicenseTypes);

        _employeeRepository.Update(employee);
        await _unitOfWork.Commit();

        return employee;
    }
}
