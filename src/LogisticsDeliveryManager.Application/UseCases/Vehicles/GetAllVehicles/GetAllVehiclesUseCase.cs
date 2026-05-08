using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;

namespace LogisticsDeliveryManager.Application.UseCases.Vehicles.GetAllVehicles;

public interface IGetAllVehiclesUseCase
{
    Task<IEnumerable<Vehicle>> Execute();
}

public class GetAllVehiclesUseCase : IGetAllVehiclesUseCase
{
    private readonly IVehicleRepository _repository;

    public GetAllVehiclesUseCase(IVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Vehicle>> Execute()
    {
        return await _repository.GetAll();
    }
}
