using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.UseCases.Customers.CreateCustomer;

public interface ICreateCustomerUseCase
{
    Task<Customer> Execute(CreateCustomerCommand command);
}
