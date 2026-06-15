using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Services.Orders;

namespace LogisticsDeliveryManager.Domain.Services.Vehicles;

public class VehicleSelectionService : IVehicleSelectionService
{
    private readonly ICargoCompatibilityPolicy _cargoCompatibilityPolicy;

    public VehicleSelectionService(ICargoCompatibilityPolicy cargoCompatibilityPolicy)
    {
        _cargoCompatibilityPolicy = cargoCompatibilityPolicy;
    }

    public Vehicle? FindBestVehicleForOrder(Order order, IEnumerable<Vehicle> availableVehicles)
    {
        var candidates = availableVehicles
            .Where(v => v.IsReadyForOrders)
            .Where(v => v.CurrentDriver != null)
            .Where(v => !v.IsFullyLoaded)
            .Where(v => v.VolumeCapacity.Value >= order.Volume.Value)
            .Where(v => v.WeightCapacity.Value >= order.Weight.Value)
            .Where(v => _cargoCompatibilityPolicy.IsVehicleCompatible(order.CargoType, v.CompartmentType));

        return candidates
            .OrderBy(v => v.VolumeCapacity.Value)
            .ThenBy(v => v.WeightCapacity.Value)
            .FirstOrDefault();
    }
}
