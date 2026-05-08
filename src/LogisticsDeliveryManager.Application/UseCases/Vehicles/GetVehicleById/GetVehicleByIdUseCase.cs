using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;

namespace LogisticsDeliveryManager.Application.UseCases.Vehicles.GetVehicleById;

public interface IGetVehicleByIdUseCase
{
    Task<Vehicle?> Execute(long id);
}

public class GetVehicleByIdUseCase : IGetVehicleByIdUseCase
{
    private readonly IVehicleRepository _repository;

    public GetVehicleByIdUseCase(IVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Vehicle?> Execute(long id)
    {
        return await _repository.GetById(id);
    }
}
