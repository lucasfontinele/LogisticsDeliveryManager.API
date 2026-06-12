using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Employees;

namespace LogisticsDeliveryManager.Application.UseCases.Drivers.GetAllDrivers;

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
        var all = await _repository.GetAll();
        return all.Where(e => e.RoleType == Domain.Enums.RoleType.Driver);
    }
}
