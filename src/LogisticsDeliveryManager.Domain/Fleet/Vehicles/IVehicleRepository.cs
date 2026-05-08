using System.Threading;

namespace LogisticsDeliveryManager.Domain.Fleet.Vehicles;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(long vehicleId, CancellationToken cancellationToken);
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task<bool> ExistsByLicensePlateAsync(LicensePlate licensePlate, CancellationToken cancellationToken);
}
