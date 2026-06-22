using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Services.Vehicles;

public interface IVehicleSelectionService
{
    Vehicle? FindBestVehicleForOrder(Order order, IEnumerable<Vehicle> availableVehicles);
}
