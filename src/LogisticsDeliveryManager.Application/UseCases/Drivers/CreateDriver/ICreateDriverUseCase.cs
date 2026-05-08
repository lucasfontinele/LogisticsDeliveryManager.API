using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.UseCases.Drivers.CreateDriver;

public interface ICreateDriverUseCase
{
    Task<Driver> Execute(CreateDriverCommand command);
}
