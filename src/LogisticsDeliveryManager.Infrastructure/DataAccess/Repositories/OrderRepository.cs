using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Orders;
using Microsoft.EntityFrameworkCore;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess.Repositories;

internal class OrderRepository : IOrderRepository
{
    private readonly LogisticsDeliveryManagerDbContext _dbContext;

    public OrderRepository(LogisticsDeliveryManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Order order)
    {
        await _dbContext.AddAsync(order);
    }

    public async Task<Order?> GetById(Guid id)
    {
        return await _dbContext.Orders
            .Include(o => o.DestinationAddress)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _dbContext.Orders
            .Include(o => o.DestinationAddress)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByCustomerId(Guid customerId)
    {
        return await _dbContext.Orders
            .Include(o => o.DestinationAddress)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByDriverId(Guid driverId)
    {
        var vehicleIdsForDriver = await _dbContext.Vehicles
            .Where(v => v.CurrentDriverId == driverId)
            .Select(v => v.Id)
            .ToListAsync();

        var orderIdsFromBatches = await _dbContext.Batches
            .Where(b => b.DriverId == driverId)
            .SelectMany(b => b.BatchOrders.Select(bo => bo.OrderId))
            .ToListAsync();

        return await _dbContext.Orders
            .Include(o => o.DestinationAddress)
            .Where(o => orderIdsFromBatches.Contains(o.Id) || (o.AssignedVehicleId != null && vehicleIdsForDriver.Contains(o.AssignedVehicleId.Value)))
            .ToListAsync();
    }

    public void Update(Order order)
    {
        _dbContext.Orders.Update(order);
    }
}
