using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Customers;

public interface ICustomerRepository
{
    Task Add(Customer customer);
    Task<Customer?> GetById(Guid id);
    Task<IEnumerable<Customer>> GetAll();
    void Update(Customer customer);
    void Delete(Customer customer);
    Task<bool> ExistActiveCustomerWithEmail(string email);
}
