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
            .Include(o => o.Customer)
            .Include(o => o.DestinationAddress)
            .Include(o => o.AssignedVehicle)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.DestinationAddress)
            .Include(o => o.AssignedVehicle)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByCustomerId(Guid customerId)
    {
        return await _dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.DestinationAddress)
            .Include(o => o.AssignedVehicle)
            .Where(o => o.Customer.Id == customerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByDriverId(Guid driverId)
    {
        // Get order IDs from batches belonging to this driver
        var orderIdsFromBatches = await _dbContext.BatchOrders
            .Where(bo => bo.Batch.Driver.Id == driverId)
            .Select(bo => bo.Order.Id)
            .ToListAsync();

        return await _dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.DestinationAddress)
            .Include(o => o.AssignedVehicle)
            .Where(o => orderIdsFromBatches.Contains(o.Id) || (o.AssignedVehicle != null && o.AssignedVehicle.CurrentDriverId == driverId))
            .ToListAsync();
    }

    public void Update(Order order)
    {
        _dbContext.Orders.Update(order);
    }
}
