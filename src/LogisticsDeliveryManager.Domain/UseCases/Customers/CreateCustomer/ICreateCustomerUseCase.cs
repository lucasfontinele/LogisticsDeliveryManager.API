using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.UseCases.Customers.CreateCustomer;

public interface ICreateCustomerUseCase
{
    Task<Customer> Execute(CreateCustomerCommand command);
}
