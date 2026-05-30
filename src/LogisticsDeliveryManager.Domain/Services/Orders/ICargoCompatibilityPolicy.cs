using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Services.Orders;

public interface ICargoCompatibilityPolicy
{
    bool IsVehicleCompatible(CargoType cargoType, CompartmentType compartmentType);
    bool CanMixCargoTypes(CargoType firstCargoType, CargoType secondCargoType);
}
