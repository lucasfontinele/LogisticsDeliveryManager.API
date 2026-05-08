namespace LogisticsDeliveryManager.Application.UseCases.Vehicles.AllocateDriver;

public class AllocateDriverToVehicleCommand
{
    public long VehicleId { get; set; }
    public long DriverId { get; set; }

    public AllocateDriverToVehicleCommand(long vehicleId, long driverId)
    {
        VehicleId = vehicleId;
        DriverId = driverId;
    }
}

public interface IAllocateDriverToVehicleUseCase
{
    Task Execute(AllocateDriverToVehicleCommand command);
}
