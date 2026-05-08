using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Batches;

namespace LogisticsDeliveryManager.Application.UseCases.Batch.GetAllBatches;

public interface IGetAllBatchesUseCase
{
    Task<IEnumerable<LogisticsDeliveryManager.Domain.Entities.Batch>> Execute();
}

public class GetAllBatchesUseCase : IGetAllBatchesUseCase
{
    private readonly IBatchRepository _repository;

    public GetAllBatchesUseCase(IBatchRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LogisticsDeliveryManager.Domain.Entities.Batch>> Execute()
    {
        return await _repository.GetAll();
    }
}
