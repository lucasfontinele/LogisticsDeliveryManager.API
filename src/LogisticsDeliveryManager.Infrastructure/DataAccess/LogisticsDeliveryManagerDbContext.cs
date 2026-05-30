using LogisticsDeliveryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess;

internal class LogisticsDeliveryManagerDbContext(DbContextOptions<LogisticsDeliveryManagerDbContext> options) : DbContext(options)
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Batch> Batches => Set<Batch>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Shipping> Shippings => Set<Shipping>();
    public DbSet<DeliveryTrackingEvent> DeliveryTrackingEvents => Set<DeliveryTrackingEvent>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LogisticsDeliveryManagerDbContext).Assembly);
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(LogisticsDeliveryManager.Domain.Entities.Base.EntityBase).IsAssignableFrom(entity.ClrType))
            {
                modelBuilder.Entity(entity.ClrType)
                    .Property("Id")
                    .ValueGeneratedOnAdd();
            }
        }

        // Global Query Filters for Soft Delete
        modelBuilder.Entity<Customer>().HasQueryFilter(e => e.IsActive);
        modelBuilder.Entity<Employee>().HasQueryFilter(e => e.IsActive);
        modelBuilder.Entity<Vehicle>().HasQueryFilter(e => e.IsActive);
        modelBuilder.Entity<Batch>().HasQueryFilter(e => e.IsActive);

        modelBuilder.Entity<Batch>().OwnsMany(b => b.BatchOrders, ownedNavBuilder =>
        {
            ownedNavBuilder.WithOwner().HasForeignKey("BatchId");
            ownedNavBuilder.HasKey("BatchId", nameof(Batch.BatchOrder.OrderId));
            ownedNavBuilder.Property(bo => bo.OrderId).IsRequired();
            ownedNavBuilder.ToTable("BatchOrders");
        });
    }
}
