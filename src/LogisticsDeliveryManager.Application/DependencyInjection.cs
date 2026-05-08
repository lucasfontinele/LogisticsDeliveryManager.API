using LogisticsDeliveryManager.Application.UseCases.Batch.CreateBatch;
using LogisticsDeliveryManager.Application.UseCases.Customers.CreateCustomer;
using LogisticsDeliveryManager.Application.UseCases.Drivers.CreateDriver;
using LogisticsDeliveryManager.Application.UseCases.Employees.CreateEmployee;
using LogisticsDeliveryManager.Application.UseCases.Orders.CreateOrder;
using LogisticsDeliveryManager.Application.UseCases.Vehicles.CreateVehicle;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsDeliveryManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);

        return services;
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
        services.AddScoped<ICreateVehicleUseCase, CreateVehicleUseCase>();
        services.AddScoped<ICreateEmployeeUseCase, CreateEmployeeUseCase>();
        services.AddScoped<ICreateDriverUseCase, CreateDriverUseCase>();
        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<ICreateBatchUseCase, CreateBatchUseCase>();
    }
}
