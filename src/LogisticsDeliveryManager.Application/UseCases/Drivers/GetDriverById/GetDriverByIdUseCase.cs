using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Drivers;

namespace LogisticsDeliveryManager.Application.UseCases.Drivers.GetDriverById;

public interface IGetDriverByIdUseCase
{
    Task<Driver?> Execute(Guid id);
}

public class GetDriverByIdUseCase : IGetDriverByIdUseCase
{
    private readonly IDriverRepository _repository;

    public GetDriverByIdUseCase(IDriverRepository repository)
    {
        _repository = repository;
    }

    public async Task<Driver?> Execute(Guid id)
    {
        return await _repository.GetById(id);
    }
}
