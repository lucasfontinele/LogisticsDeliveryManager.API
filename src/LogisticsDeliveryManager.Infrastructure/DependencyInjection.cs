using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Batches;
using LogisticsDeliveryManager.Domain.Repositories.Customers;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using LogisticsDeliveryManager.Domain.Repositories.Employees;
using LogisticsDeliveryManager.Domain.Repositories.Orders;
using LogisticsDeliveryManager.Infrastructure.Configuration;using LogisticsDeliveryManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsDeliveryManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<LogisticsDeliveryManagerDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICustomerRepository, DataAccess.Repositories.CustomerRepository>();
        services.AddScoped<IVehicleRepository, DataAccess.Repositories.VehicleRepository>();
        services.AddScoped<IEmployeeRepository, DataAccess.Repositories.EmployeeRepository>();
        services.AddScoped<LogisticsDeliveryManager.Domain.Services.Vehicles.IVehicleSelectionService, LogisticsDeliveryManager.Domain.Services.Vehicles.VehicleSelectionService>();
        services.AddScoped<IOrderRepository, DataAccess.Repositories.OrderRepository>();
        services.AddScoped<IBatchRepository, DataAccess.Repositories.BatchRepository>();

        return services;
    }

    public static void InitializeDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LogisticsDeliveryManagerDbContext>();
        dbContext.Database.EnsureCreated();
        
        // Add all missing columns
        var connection = dbContext.Database.GetDbConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE EXTENSION IF NOT EXISTS pgcrypto;

            -- Employees table
            ALTER TABLE ""Employees"" ADD COLUMN IF NOT EXISTS ""LicenseTypes"" integer[] NOT NULL DEFAULT '{}';
            
            -- Batches table
            ALTER TABLE ""Batches"" ADD COLUMN IF NOT EXISTS ""VehicleWeightCapacity_Value"" double precision NOT NULL DEFAULT 0;
            ALTER TABLE ""Batches"" ADD COLUMN IF NOT EXISTS ""VehicleVolumeCapacity_Value"" double precision NOT NULL DEFAULT 0;
            ALTER TABLE ""Batches"" DROP CONSTRAINT IF EXISTS ""FK_Batches_Drivers_DriverId"";
            DO $$
            BEGIN
                IF NOT EXISTS (
                    SELECT 1
                    FROM pg_constraint
                    WHERE conname = 'FK_Batches_Employees_DriverId'
                ) THEN
                    ALTER TABLE ""Batches""
                    ADD CONSTRAINT ""FK_Batches_Employees_DriverId""
                    FOREIGN KEY (""DriverId"") REFERENCES ""Employees"" (""Id"");
                END IF;
            END $$;
            
            -- Vehicles table
            ALTER TABLE ""Vehicles"" ADD COLUMN IF NOT EXISTS ""LicensePlate_Value"" text;
            ALTER TABLE ""Vehicles"" ADD COLUMN IF NOT EXISTS ""WeightCapacity_Value"" double precision NOT NULL DEFAULT 0;
            ALTER TABLE ""Vehicles"" ADD COLUMN IF NOT EXISTS ""VolumeCapacity_Value"" double precision NOT NULL DEFAULT 0;
            
            -- Orders table
            ALTER TABLE ""Orders"" ADD COLUMN IF NOT EXISTS ""Weight_Value"" double precision NOT NULL DEFAULT 0;
            ALTER TABLE ""Orders"" ADD COLUMN IF NOT EXISTS ""Volume_Value"" double precision NOT NULL DEFAULT 0;
            ALTER TABLE ""Orders"" ADD COLUMN IF NOT EXISTS ""DeliveryWindow_Start"" timestamp with time zone NOT NULL DEFAULT NOW();
            ALTER TABLE ""Orders"" ADD COLUMN IF NOT EXISTS ""DeliveryWindow_End"" timestamp with time zone NOT NULL DEFAULT NOW();
            ALTER TABLE ""Orders"" ADD COLUMN IF NOT EXISTS ""DestinationAddress_Street"" text NOT NULL DEFAULT '';
            ALTER TABLE ""Orders"" ADD COLUMN IF NOT EXISTS ""DestinationAddress_City"" text NOT NULL DEFAULT '';
            ALTER TABLE ""Orders"" ADD COLUMN IF NOT EXISTS ""DestinationAddress_State"" text NOT NULL DEFAULT '';
            ALTER TABLE ""Orders"" ADD COLUMN IF NOT EXISTS ""DestinationAddress_PostalCode_Code"" text NOT NULL DEFAULT '';
            
            -- BatchOrders table
            ALTER TABLE ""BatchOrders"" ADD COLUMN IF NOT EXISTS ""Weight_Value"" double precision NOT NULL DEFAULT 0;
            ALTER TABLE ""BatchOrders"" ADD COLUMN IF NOT EXISTS ""Volume_Value"" double precision NOT NULL DEFAULT 0;
            DO $$
            BEGIN
                IF EXISTS (
                    SELECT 1
                    FROM information_schema.columns
                    WHERE table_name = 'BatchOrders'
                      AND column_name = 'Id'
                      AND data_type = 'uuid'
                ) THEN
                    ALTER TABLE ""BatchOrders""
                    ALTER COLUMN ""Id"" SET DEFAULT gen_random_uuid();
                END IF;
            END $$;
            DO $$
            BEGIN
                IF EXISTS (
                    SELECT 1
                    FROM information_schema.columns
                    WHERE table_name = 'BatchOrders'
                      AND column_name = 'CreatedAt'
                ) THEN
                    ALTER TABLE ""BatchOrders""
                    ALTER COLUMN ""CreatedAt"" SET DEFAULT NOW();
                END IF;
            END $$;
            DO $$
            BEGIN
                IF EXISTS (
                    SELECT 1
                    FROM information_schema.columns
                    WHERE table_name = 'BatchOrders'
                      AND column_name = 'IsActive'
                ) THEN
                    ALTER TABLE ""BatchOrders""
                    ALTER COLUMN ""IsActive"" SET DEFAULT TRUE;
                END IF;
            END $$;
            
            -- Addresses table (owned by Customer)
            ALTER TABLE ""Addresses"" ADD COLUMN IF NOT EXISTS ""PostalCode_Code"" text NOT NULL DEFAULT '';
        ";
        command.ExecuteNonQuery();
        connection.Close();
    }
}
