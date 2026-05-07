namespace LogisticsDeliveryManager.Domain.Services.Vehicles;

public interface IVehicleDomainService
{
    Task ValidateUniqueLicensePlate(string licensePlate);
}
