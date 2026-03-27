using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Customers;
using LogisticsDeliveryManager.Infrastructure.Configuration;
using LogisticsDeliveryManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsDeliveryManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionUrl = configuration[SupabaseSettings.ConnectionUrlKey];
        var connectionString = SupabaseConnectionStringFactory.Build("postgresql://postgres:WeB5iqH7ObOQqC41@db.gqnonkdrzzpjgshgakdy.supabase.co:5432/postgres?sslmode=require");

        services.AddDbContext<LogisticsDeliveryManagerDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICustomerRepository, DataAccess.Repositories.CustomerRepository>();

        return services;
    }
}
