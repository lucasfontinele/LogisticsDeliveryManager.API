using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Domain.Services.Orders;

public interface IOrderRoutingDomainService
{
    (DateTime Start, DateTime End) CalculateSla(bool isPriority);
    Vehicle? FindBestVehicleForOrder(Order order, IEnumerable<Vehicle> availableVehicles);
}
