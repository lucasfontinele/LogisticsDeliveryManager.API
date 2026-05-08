namespace LogisticsDeliveryManager.Application.UseCases.Vehicles.AllocateDriver;

public class AllocateDriverToVehicleCommand
{
    public Guid VehicleId { get; set; }
    public Guid DriverId { get; set; }

    public AllocateDriverToVehicleCommand(Guid vehicleId, Guid driverId)
    {
        VehicleId = vehicleId;
        DriverId = driverId;
    }
}

public interface IAllocateDriverToVehicleUseCase
{
    Task Execute(AllocateDriverToVehicleCommand command);
}
