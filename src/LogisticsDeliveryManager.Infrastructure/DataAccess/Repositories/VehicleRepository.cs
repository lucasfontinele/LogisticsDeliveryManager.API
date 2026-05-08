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

        public async Task<Vehicle?> GetById(Guid id)
        {
            return await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public void Update(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
        }

        public void Delete(Vehicle vehicle)
        {
            vehicle.Deactivate();
            _dbContext.Vehicles.Update(vehicle);
        }

        public async Task<bool> ExistActiveVehicleWithLicensePlate(string licensePlate)
        {
            return await _dbContext.Vehicles.AnyAsync(v => v.LicensePlate == licensePlate);
        }
    }
}
