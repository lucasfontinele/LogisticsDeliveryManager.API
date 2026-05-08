using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Employees;

public interface IEmployeeRepository
{
    Task Add(Employee employee);
    Task<Employee?> GetById(Guid id);
    Task<IEnumerable<Employee>> GetAll();
    void Update(Employee employee);
    void Delete(Employee employee);
    Task<bool> ExistActiveEmployeeWithEmail(string email);
}
