using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using System.Linq;

namespace LogisticsDeliveryManager.Domain.Services.Orders;

public class OrderRoutingDomainService : IOrderRoutingDomainService
{
    public (DateTime Start, DateTime End) CalculateSla(bool isPriority)
    {
        var start = DateTime.UtcNow;
        var end = isPriority ? start.AddDays(1) : start.AddDays(3);
        return (start, end);
    }

    public Vehicle? FindBestVehicleForOrder(Order order, IEnumerable<Vehicle> availableVehicles)
    {
        var candidates = availableVehicles
            .Where(v => v.IsReadyForOrders)
            .Where(v => v.CurrentDriver != null)
            .Where(v => !v.IsFullyLoaded)
            .Where(v => v.VolumeCapacity >= order.Volume)
            .Where(v => v.WeightCapacity >= order.Weight);

        if (order.CargoType == CargoType.Medicine || order.CargoType == CargoType.Refrigerated)
        {
            candidates = candidates.Where(v => v.CompartmentType == CompartmentType.RefrigeratedBody);
        }

        return candidates
            .OrderBy(v => v.VolumeCapacity)
            .ThenBy(v => v.WeightCapacity)
            .FirstOrDefault();
    }
}
