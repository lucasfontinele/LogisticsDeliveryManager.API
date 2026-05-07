using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Services.Vehicles;

public class VehicleDomainService : IVehicleDomainService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleDomainService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task ValidateUniqueLicensePlate(string licensePlate)
    {
        var licensePlateExist = await _vehicleRepository.ExistActiveVehicleWithLicensePlate(licensePlate);
        if (licensePlateExist)
        {
            throw new ErrorOnValidationException(new List<string> { "Vehicle with this license plate already registered" });
        }
    }
}
