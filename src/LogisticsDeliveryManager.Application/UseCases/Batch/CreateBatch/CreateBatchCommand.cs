using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Application.UseCases.Batch.CreateBatch;

public class CreateBatchCommand
{
    public CargoType Type { get; private set; }
    public Guid DriverId { get; private set; }
    public Guid VehicleId { get; private set; }
    public DateOnly DeliveryDate { get; private set; }
    
    public CreateBatchCommand(CargoType type, Guid vehicleId, Guid driverId, DateOnly deliveryDate)
    {
        Type = type;
        VehicleId = vehicleId;
        DriverId = driverId;
        DeliveryDate = deliveryDate;
    }
}
