using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Employees;

namespace LogisticsDeliveryManager.Application.UseCases.Employees.GetAllEmployees;

public interface IGetAllEmployeesUseCase
{
    Task<IEnumerable<Employee>> Execute();
}

public class GetAllEmployeesUseCase : IGetAllEmployeesUseCase
{
    private readonly IEmployeeRepository _repository;

    public GetAllEmployeesUseCase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Employee>> Execute()
    {
        return await _repository.GetAll();
    }
}
