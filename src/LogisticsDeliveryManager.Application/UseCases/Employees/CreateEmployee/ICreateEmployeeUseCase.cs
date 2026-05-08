using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.UseCases.Employees.CreateEmployee;

public interface ICreateEmployeeUseCase
{
    Task<Employee> Execute(CreateEmployeeCommand command);
}
