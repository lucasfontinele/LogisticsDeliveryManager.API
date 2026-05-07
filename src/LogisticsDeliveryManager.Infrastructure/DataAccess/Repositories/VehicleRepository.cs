using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess.Repositories
{
    internal class VehicleRepository : IVehicleRepository
    {
        private readonly LogisticsDeliveryManagerDbContext _dbContext;

        public VehicleRepository(LogisticsDeliveryManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Vehicle vehicle)
        {
            await _dbContext.AddAsync(vehicle);
        }

        public async Task<bool> ExistActiveVehicleWithLicensePlate(string licensePlate)
        {
            return await _dbContext.Vehicles.AnyAsync(v => v.LicensePlate == licensePlate);
        }
    }
}
