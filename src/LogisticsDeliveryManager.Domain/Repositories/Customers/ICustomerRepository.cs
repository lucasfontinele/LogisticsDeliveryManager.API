using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Customers;

public interface ICustomerRepository
{
    Task Add(Customer customer);
    Task<bool> ExistActiveCustomerWithEmail(string email);
}
