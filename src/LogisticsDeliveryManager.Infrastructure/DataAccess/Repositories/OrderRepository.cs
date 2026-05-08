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

    public async Task<Order?> GetById(long id)
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
            .Include(o => o.DestinationAddress)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByCustomerId(long customerId)
    {
        return await _dbContext.Orders
            .Include(o => o.DestinationAddress)
            .Where(o => o.Customer.Id == customerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByDriverId(long driverId)
    {
        return await _dbContext.Orders
            .Include(o => o.DestinationAddress)
            .Where(o => o.AssignedVehicle != null && o.AssignedVehicle.CurrentDriver != null && o.AssignedVehicle.CurrentDriver.Id == driverId)
            .ToListAsync();
    }

    public void Update(Order order)
    {
        _dbContext.Orders.Update(order);
    }
}
