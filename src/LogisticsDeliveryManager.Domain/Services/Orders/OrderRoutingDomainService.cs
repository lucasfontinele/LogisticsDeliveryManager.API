using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using System.Linq;

namespace LogisticsDeliveryManager.Domain.Services.Orders;

public class OrderRoutingDomainService : IOrderRoutingDomainService
{
    private readonly IDeliveryPromiseService _deliveryPromiseService;
    private readonly ICargoCompatibilityPolicy _cargoCompatibilityPolicy;

    public OrderRoutingDomainService(
        IDeliveryPromiseService deliveryPromiseService,
        ICargoCompatibilityPolicy cargoCompatibilityPolicy)
    {
        _deliveryPromiseService = deliveryPromiseService;
        _cargoCompatibilityPolicy = cargoCompatibilityPolicy;
    }

    public (DateTime Start, DateTime End) CalculateSla(bool isPriority)
    {
        var window = _deliveryPromiseService.CalculateDeliveryPromise(isPriority);
        return (window.Start, window.End);
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
