using LogisticsDeliveryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess;

internal class LogisticsDeliveryManagerDbContext(DbContextOptions<LogisticsDeliveryManagerDbContext> options) : DbContext(options)
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Batch> Batches => Set<Batch>();
    public DbSet<BatchOrder> BatchOrders => Set<BatchOrder>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Shipping> Shippings => Set<Shipping>();
    public DbSet<ShippingStatuses> ShippingStatuses => Set<ShippingStatuses>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LogisticsDeliveryManagerDbContext).Assembly);
    }
}
