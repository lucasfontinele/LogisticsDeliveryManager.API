using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Drivers;

public interface IDriverRepository
{
    Task Add(Driver driver);
    Task<Driver?> GetById(Guid id);
    Task<IEnumerable<Driver>> GetAll();
    Task<bool> ExistDriverForEmployee(Guid employeeId);
    Task<Driver?> GetByEmployeeId(Guid employeeId);
}
