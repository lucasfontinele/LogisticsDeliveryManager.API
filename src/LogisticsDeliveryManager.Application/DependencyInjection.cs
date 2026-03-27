using LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer;
using Microsoft.Extensions.DependencyInjection;

namespace LogisticsDeliveryManager.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            RegisterUseCases(services);
        }

        private static void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
        }
    }
}
