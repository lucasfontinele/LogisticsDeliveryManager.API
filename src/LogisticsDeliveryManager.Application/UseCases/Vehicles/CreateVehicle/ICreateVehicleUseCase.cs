using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.UseCases.Vehicles.CreateVehicle;

public interface ICreateVehicleUseCase
{
    Task<Vehicle> Execute(CreateVehicleCommand command);
}
