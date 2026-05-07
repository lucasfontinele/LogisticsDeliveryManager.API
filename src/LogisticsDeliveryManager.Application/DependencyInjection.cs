using LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer;
using LogisticsDeliveryManager.Application.UseCases.Vehicle.CreateVehicle;
using LogisticsDeliveryManager.Domain.Services.Customers;
using LogisticsDeliveryManager.Domain.Services.Vehicles;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsDeliveryManager.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            RegisterUseCases(services);
            RegisterDomainServices(services);
        }

        private static void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
            services.AddScoped<ICreateVehicleUseCase, CreateVehicleUseCase>();
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerDomainService, CustomerDomainService>();
            services.AddScoped<IVehicleDomainService, VehicleDomainService>();
        }
    }
}
