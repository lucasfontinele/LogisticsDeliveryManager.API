using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Drivers;

public interface IDriverRepository
{
    Task Add(Driver driver);
    Task<Driver?> GetById(long id);
    Task<IEnumerable<Driver>> GetAll();
    Task<bool> ExistDriverForEmployee(long employeeId);
}
