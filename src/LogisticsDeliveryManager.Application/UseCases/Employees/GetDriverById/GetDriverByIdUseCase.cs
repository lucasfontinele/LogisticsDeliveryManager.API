using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Repositories.Employees;

namespace LogisticsDeliveryManager.Application.UseCases.Employees.GetDriverById;

public interface IGetDriverByIdUseCase
{
    Task<Employee?> Execute(Guid id);
}

public class GetDriverByIdUseCase : IGetDriverByIdUseCase
{
    private readonly IEmployeeRepository _repository;

    public GetDriverByIdUseCase(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Employee?> Execute(Guid id)
    {
        var employee = await _repository.GetById(id);
        if (employee is null)
            return null;

        return employee.RoleType == RoleType.Driver ? employee : null;
    }
}
