using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.UseCases.Drivers.CreateDriver;

public interface ICreateDriverUseCase
{
    Task<Employee> Execute(CreateDriverCommand command);
}
