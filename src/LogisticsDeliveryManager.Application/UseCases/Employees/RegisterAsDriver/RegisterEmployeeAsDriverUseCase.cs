using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Employees;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Employees.RegisterAsDriver;

public interface IRegisterEmployeeAsDriverUseCase
{
    Task<Employee> Execute(RegisterEmployeeAsDriverCommand command);
}

public class RegisterEmployeeAsDriverUseCase : IRegisterEmployeeAsDriverUseCase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterEmployeeAsDriverUseCase(
        IEmployeeRepository employeeRepository,
        IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Employee> Execute(RegisterEmployeeAsDriverCommand command)
    {
        var employee = await _employeeRepository.GetById(command.EmployeeId);
        if (employee is null)
            throw new ErrorOnValidationException(["Employee not found."]);

        if (employee.RoleType == RoleType.Driver)
            throw new ErrorOnValidationException(["This employee is already registered as a driver."]);

        employee.RegisterAsDriver(command.LicenseTypes);

        _employeeRepository.Update(employee);
        await _unitOfWork.Commit();

        return employee;
    }
}
