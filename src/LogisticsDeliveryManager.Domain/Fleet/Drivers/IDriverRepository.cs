using System.Threading;

namespace LogisticsDeliveryManager.Domain.Fleet.Drivers;

public interface IDriverRepository
{
    Task<Driver?> GetByIdAsync(long driverId, CancellationToken cancellationToken);
    Task AddAsync(Driver driver, CancellationToken cancellationToken);
    Task UpdateAsync(Driver driver, CancellationToken cancellationToken);
}
