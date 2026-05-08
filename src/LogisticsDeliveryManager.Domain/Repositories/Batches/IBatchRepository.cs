using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Repositories.Batches;

public interface IBatchRepository
{
    Task Add(Batch batch);
    Task<Batch?> GetById(long id);
    Task<IEnumerable<Batch>> GetAll();
    void Update(Batch batch);
    void Delete(Batch batch);
}
