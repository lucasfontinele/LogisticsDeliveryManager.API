using LogisticsDeliveryManager.Application.UseCases.Batch.CreateBatch;
using LogisticsDeliveryManager.Application.UseCases.Batch.AddOrderToBatch;
using LogisticsDeliveryManager.Application.UseCases.Batch.GetAllBatches;
using LogisticsDeliveryManager.Application.UseCases.Batch.GetBatchById;
using LogisticsDeliveryManager.Application.UseCases.Customers.CreateCustomer;
using LogisticsDeliveryManager.Application.UseCases.Employees.CreateEmployee;
using LogisticsDeliveryManager.Application.UseCases.Employees.GetAllDrivers;
using LogisticsDeliveryManager.Application.UseCases.Employees.GetDriverById;
using LogisticsDeliveryManager.Application.UseCases.Employees.RegisterAsDriver;
using LogisticsDeliveryManager.Application.UseCases.Orders.CreateOrder;
using LogisticsDeliveryManager.Application.UseCases.Vehicles.CreateVehicle;
using LogisticsDeliveryManager.Application.UseCases.Orders.GetAllOrders;
using LogisticsDeliveryManager.Application.UseCases.Orders.GetOrderById;
using LogisticsDeliveryManager.Application.UseCases.Customers.GetCustomerById;
using LogisticsDeliveryManager.Application.UseCases.Vehicles.GetVehicleById;
using LogisticsDeliveryManager.Application.UseCases.Employees.GetEmployeeById;
using LogisticsDeliveryManager.Application.UseCases.Orders.AssignVehicle;
using LogisticsDeliveryManager.Domain.Factories;
using LogisticsDeliveryManager.Domain.Services.Drivers;
using LogisticsDeliveryManager.Domain.Services.Orders;
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
        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IRegisterEmployeeAsDriverUseCase, RegisterEmployeeAsDriverUseCase>();
        services.AddScoped<UseCases.Vehicles.AllocateDriver.IAllocateDriverToVehicleUseCase, UseCases.Vehicles.AllocateDriver.AllocateDriverToVehicleUseCase>();
        services.AddScoped<UseCases.Orders.UpdateStatus.IUpdateOrderStatusUseCase, UseCases.Orders.UpdateStatus.UpdateOrderStatusUseCase>();
        services.AddScoped<UseCases.Orders.UploadProof.IUploadOrderProofUseCase, UseCases.Orders.UploadProof.UploadOrderProofUseCase>();
        services.AddScoped<UseCases.Orders.Evaluate.IEvaluateOrderUseCase, UseCases.Orders.Evaluate.EvaluateOrderUseCase>();
        services.AddScoped<IAssignOrderToVehicleUseCase, AssignOrderToVehicleUseCase>();
        services.AddScoped<UseCases.Orders.GetCustomerOrders.IGetCustomerOrdersUseCase, UseCases.Orders.GetCustomerOrders.GetCustomerOrdersUseCase>();
        services.AddScoped<UseCases.Orders.GetDriverOrders.IGetDriverOrdersUseCase, UseCases.Orders.GetDriverOrders.GetDriverOrdersUseCase>();
        services.AddScoped<IGetAllOrdersUseCase, GetAllOrdersUseCase>();
        services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
        services.AddScoped<UseCases.Customers.GetAllCustomers.IGetAllCustomersUseCase, UseCases.Customers.GetAllCustomers.GetAllCustomersUseCase>();
        services.AddScoped<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
        services.AddScoped<UseCases.Vehicles.GetAllVehicles.IGetAllVehiclesUseCase, UseCases.Vehicles.GetAllVehicles.GetAllVehiclesUseCase>();
        services.AddScoped<IGetVehicleByIdUseCase, GetVehicleByIdUseCase>();
        services.AddScoped<UseCases.Employees.GetAllEmployees.IGetAllEmployeesUseCase, UseCases.Employees.GetAllEmployees.GetAllEmployeesUseCase>();
        services.AddScoped<IGetEmployeeByIdUseCase, GetEmployeeByIdUseCase>();
        services.AddScoped<IGetAllDriversUseCase, GetAllDriversUseCase>();
        services.AddScoped<IGetDriverByIdUseCase, GetDriverByIdUseCase>();
        services.AddScoped<ICreateBatchUseCase, CreateBatchUseCase>();
        services.AddScoped<IAddOrderToBatchUseCase, AddOrderToBatchUseCase>();
        services.AddScoped<IGetAllBatchesUseCase, GetAllBatchesUseCase>();
        services.AddScoped<IGetBatchByIdUseCase, GetBatchByIdUseCase>();
        services.AddScoped<IOrderRoutingDomainService, OrderRoutingDomainService>();
        services.AddScoped<IDeliveryPromiseService, DeliveryPromiseService>();
        services.AddScoped<ICargoCompatibilityPolicy, CargoCompatibilityPolicy>();
        services.AddScoped<IDriverJourneyPolicy, DriverJourneyPolicy>();
        services.AddScoped<OrderFactory>();
    }
}
