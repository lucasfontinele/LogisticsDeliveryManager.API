using DomainBatch = LogisticsDeliveryManager.Domain.Entities.Batch;

namespace LogisticsDeliveryManager.Application.UseCases.Batch.CreateBatch;

public interface ICreateBatchUseCase
{
    Task<DomainBatch> Execute(CreateBatchCommand command);
}
