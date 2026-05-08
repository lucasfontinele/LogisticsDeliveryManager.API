using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Batches;

namespace LogisticsDeliveryManager.Application.UseCases.Batch.GetBatchById;

public interface IGetBatchByIdUseCase
{
    Task<LogisticsDeliveryManager.Domain.Entities.Batch?> Execute(Guid id);
}

public class GetBatchByIdUseCase : IGetBatchByIdUseCase
{
    private readonly IBatchRepository _repository;

    public GetBatchByIdUseCase(IBatchRepository repository)
    {
        _repository = repository;
    }

    public async Task<LogisticsDeliveryManager.Domain.Entities.Batch?> Execute(Guid id)
    {
        return await _repository.GetById(id);
    }
}
