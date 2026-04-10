using LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer;
using LogisticsDeliveryManager.Domain.Services.Customers;
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
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerDomainService, CustomerDomainService>();
        }
    }
}
