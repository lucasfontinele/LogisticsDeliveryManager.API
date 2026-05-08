using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Drivers;
using Microsoft.EntityFrameworkCore;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess.Repositories;

internal class DriverRepository : IDriverRepository
{
    private readonly LogisticsDeliveryManagerDbContext _dbContext;

    public DriverRepository(LogisticsDeliveryManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Driver driver)
    {
        await _dbContext.AddAsync(driver);
    }

    public async Task<Driver?> GetById(long id)
    {
        return await _dbContext.Drivers
            .Include(d => d.Employee)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Driver>> GetAll()
    {
        return await _dbContext.Drivers
            .Include(d => d.Employee)
            .ToListAsync();
    }

    public async Task<bool> ExistDriverForEmployee(long employeeId)
    {
        return await _dbContext.Drivers.AnyAsync(d => d.Employee.Id == employeeId);
    }

    public async Task<Driver?> GetByEmployeeId(long employeeId)
    {
        return await _dbContext.Drivers
            .Include(d => d.Employee)
            .FirstOrDefaultAsync(d => d.Employee.Id == employeeId);
    }
}
