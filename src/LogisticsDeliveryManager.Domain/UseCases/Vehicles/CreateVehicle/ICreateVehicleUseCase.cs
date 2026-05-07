using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.UseCases.Vehicles.CreateVehicle;

public interface ICreateVehicleUseCase
{
    Task<Vehicle> Execute(CreateVehicleCommand command);
}
