using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Drivers;

namespace LogisticsDeliveryManager.Application.UseCases.Drivers.GetAllDrivers;

public interface IGetAllDriversUseCase
{
    Task<IEnumerable<Driver>> Execute();
}

public class GetAllDriversUseCase : IGetAllDriversUseCase
{
    private readonly IDriverRepository _repository;

    public GetAllDriversUseCase(IDriverRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Driver>> Execute()
    {
        return await _repository.GetAll();
    }
}
