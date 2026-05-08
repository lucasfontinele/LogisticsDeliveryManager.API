using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Vehicles
{
    public interface IVehicleRepository
    {
        Task Add(Vehicle vehicle);
        Task<Vehicle?> GetById(long id);
        Task<IEnumerable<Vehicle>> GetAll();
        void Update(Vehicle vehicle);
        void Delete(Vehicle vehicle);
        Task<bool> ExistActiveVehicleWithLicensePlate(string licensePlate);
    }
}
