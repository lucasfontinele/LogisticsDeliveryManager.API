using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Repositories.Employees;

namespace LogisticsDeliveryManager.Application.UseCases.Employees.GetAllDrivers;

public interface IGetAllDriversUseCase
{
    Task<IEnumerable<Employee>> Execute();
}

public class GetAllDriversUseCase : IGetAllDriversUseCase
{
    private readonly IEmployeeRepository _repository;

    public GetAllDriversUseCase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Employee>> Execute()
    {
        var allEmployees = await _repository.GetAll();
        return allEmployees.Where(employee => employee.RoleType == RoleType.Driver);
    }
}
