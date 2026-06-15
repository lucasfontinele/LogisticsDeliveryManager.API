using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using System.Linq;

namespace LogisticsDeliveryManager.Domain.Services.Orders;

public class OrderRoutingDomainService : IOrderRoutingDomainService
{
    private readonly IDeliveryPromiseService _deliveryPromiseService;
    private readonly ICargoCompatibilityPolicy _cargoCompatibilityPolicy;
    private readonly LogisticsDeliveryManager.Domain.Services.Vehicles.IVehicleSelectionService _vehicleSelectionService;

    public OrderRoutingDomainService(
        IDeliveryPromiseService deliveryPromiseService,
        ICargoCompatibilityPolicy cargoCompatibilityPolicy,
        LogisticsDeliveryManager.Domain.Services.Vehicles.IVehicleSelectionService vehicleSelectionService)
    {
        _deliveryPromiseService = deliveryPromiseService;
        _cargoCompatibilityPolicy = cargoCompatibilityPolicy;
        _vehicleSelectionService = vehicleSelectionService;
    }

    public (DateTime Start, DateTime End) CalculateSla(bool isPriority)
    {
        var window = _deliveryPromiseService.CalculateDeliveryPromise(isPriority);
        return (window.Start, window.End);
    }

    public Vehicle? FindBestVehicleForOrder(Order order, IEnumerable<Vehicle> availableVehicles)
    {
        return _vehicleSelectionService.FindBestVehicleForOrder(order, availableVehicles);
    }
}
