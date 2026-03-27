using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Customers;

public interface ICustomerRepository
{
    void Add(Customer customer);
}
