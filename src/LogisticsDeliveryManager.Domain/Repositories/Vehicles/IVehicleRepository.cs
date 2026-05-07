using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Vehicles
{
    public interface IVehicleRepository
    {
        Task Add(Vehicle vehicle);
        Task<bool> ExistActiveVehicleWithLicensePlate(string licensePlate);
    }
}
