using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Employees;
using LogisticsDeliveryManager.Domain.Services.Employees;
using LogisticsDeliveryManager.Domain.ValueObjects;

namespace LogisticsDeliveryManager.Application.UseCases.Employees.CreateEmployee;

public class CreateEmployeeUseCase : ICreateEmployeeUseCase
{
    private readonly IEmployeeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeDomainService _domainService;

    public CreateEmployeeUseCase(
        IEmployeeRepository repository,
        IUnitOfWork unitOfWork,
        IEmployeeDomainService domainService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _domainService = domainService;
    }

    public async Task<Employee> Execute(CreateEmployeeCommand command)
    {
        await _domainService.ValidateUniqueEmail(command.Email);

        var employee = new Employee(
            command.Name,
            command.Document,
            command.PhoneNumber,
            new Email(command.Email),
            command.RoleType);

        await _repository.Add(employee);
        await _unitOfWork.Commit();

        return employee;
    }
}
