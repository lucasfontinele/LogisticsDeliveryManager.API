using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Employees;

namespace LogisticsDeliveryManager.Application.UseCases.Employees.GetEmployeeById;

public interface IGetEmployeeByIdUseCase
{
    Task<Employee?> Execute(long id);
}

public class GetEmployeeByIdUseCase : IGetEmployeeByIdUseCase
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeeByIdUseCase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Employee?> Execute(long id)
    {
        return await _repository.GetById(id);
    }
}
