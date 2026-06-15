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
        dbContext.Database.Migrate();
    }
}
