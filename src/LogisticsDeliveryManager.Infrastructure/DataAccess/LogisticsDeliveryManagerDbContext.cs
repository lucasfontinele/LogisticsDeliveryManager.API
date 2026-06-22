using LogisticsDeliveryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess;

internal class LogisticsDeliveryManagerDbContext(DbContextOptions<LogisticsDeliveryManagerDbContext> options) : DbContext(options)
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Batch> Batches => Set<Batch>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Shipping> Shippings => Set<Shipping>();
    public DbSet<DeliveryTrackingEvent> DeliveryTrackingEvents => Set<DeliveryTrackingEvent>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

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

        modelBuilder.Entity<Customer>().OwnsMany(c => c.Addresses, a =>
        {
            a.WithOwner().HasForeignKey("CustomerId");
            a.Property<long>("Id").ValueGeneratedOnAdd();
            a.HasKey("Id");
            a.ToTable("Addresses");
        });

        modelBuilder.Entity<Batch>().OwnsMany(b => b.BatchOrders, ownedNavBuilder =>
        {
            ownedNavBuilder.WithOwner().HasForeignKey("BatchId");
            ownedNavBuilder.HasKey("BatchId", nameof(Batch.BatchOrder.OrderId));
            ownedNavBuilder.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");
            ownedNavBuilder.Property<DateTime>("CreatedAt")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NOW()");
            ownedNavBuilder.Property<bool>("IsActive")
                .ValueGeneratedOnAdd()
                .HasDefaultValue(true);
            ownedNavBuilder.Property(bo => bo.OrderId).IsRequired();
            ownedNavBuilder.ToTable("BatchOrders");
        });

        modelBuilder.Entity<Vehicle>().ComplexProperty(v => v.LicensePlate, licensePlateBuilder =>
        {
            licensePlateBuilder.Property(lp => lp.Value)
                .HasColumnName("LicensePlate")
                .IsRequired();
        });

        modelBuilder.Entity<Vehicle>().ComplexProperty(v => v.WeightCapacity, weightBuilder =>
        {
            weightBuilder.Property(w => w.Value)
                .HasColumnName("WeightCapacity")
                .IsRequired();
        });

        modelBuilder.Entity<Vehicle>().ComplexProperty(v => v.VolumeCapacity, volumeBuilder =>
        {
            volumeBuilder.Property(v => v.Value)
                .HasColumnName("VolumeCapacity")
                .IsRequired();
        });

        // Configure LicenseTypes as PostgreSQL array
        modelBuilder.Entity<Employee>()
            .Property(e => e.LicenseTypes)
            .HasColumnType("integer[]");
    }
}
