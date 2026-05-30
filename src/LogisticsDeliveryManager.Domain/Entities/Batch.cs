using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Batch : EntityBase
{
    private readonly List<BatchOrder> _batchOrders = new();

    public CargoType Type { get; private set; }
    public Guid DriverId { get; private set; }
    public Guid VehicleId { get; private set; }
    public Weight VehicleWeightCapacity { get; private set; }
    public Volume VehicleVolumeCapacity { get; private set; }
    public DateOnly DeliveryDate { get; private set; }

    public IReadOnlyCollection<Guid> OrderIds => _batchOrders.Select(bo => bo.OrderId).ToList().AsReadOnly();
    internal IReadOnlyCollection<BatchOrder> BatchOrders => _batchOrders.AsReadOnly();

    internal sealed record BatchOrder(Guid OrderId, Weight Weight, Volume Volume);

    private Batch() { }

    public Batch(CargoType type, Guid driverId, Guid vehicleId, Weight vehicleWeightCapacity, Volume vehicleVolumeCapacity, DateOnly deliveryDate)
    {
        Validate(type, driverId, vehicleId, vehicleWeightCapacity, vehicleVolumeCapacity, deliveryDate);

        Type = type;
        DriverId = driverId;
        VehicleId = vehicleId;
        VehicleWeightCapacity = vehicleWeightCapacity;
        VehicleVolumeCapacity = vehicleVolumeCapacity;
        DeliveryDate = deliveryDate;
    }

    public void AddOrder(Guid orderId, Weight orderWeight, Volume orderVolume)
    {
        if (orderId == Guid.Empty)
            throw new ErrorOnValidationException(["Order id cannot be empty."]);

        if (_batchOrders.Any(bo => bo.OrderId == orderId))
            throw new ErrorOnValidationException(["Order is already in this batch."]);

        var totalWeightAfterAdd = _batchOrders.Sum(bo => bo.Weight.Value) + orderWeight.Value;
        if (totalWeightAfterAdd > VehicleWeightCapacity.Value)
        {
            throw new ErrorOnValidationException([
                $"Batch weight capacity exceeded. Current: {_batchOrders.Sum(bo => bo.Weight.Value)}, " +
                $"Adding: {orderWeight.Value}, Capacity: {VehicleWeightCapacity.Value}."
            ]);
        }

        var totalVolumeAfterAdd = _batchOrders.Sum(bo => bo.Volume.Value) + orderVolume.Value;
        if (totalVolumeAfterAdd > VehicleVolumeCapacity.Value)
        {
            throw new ErrorOnValidationException([
                $"Batch volume capacity exceeded. Current: {_batchOrders.Sum(bo => bo.Volume.Value)}, " +
                $"Adding: {orderVolume.Value}, Capacity: {VehicleVolumeCapacity.Value}."
            ]);
        }

        _batchOrders.Add(new BatchOrder(orderId, orderWeight, orderVolume));
    }

    public void ChangeDeliveryDate(DateOnly deliveryDate)
    {
        if (deliveryDate < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ErrorOnValidationException(["Delivery date cannot be in the past."]);

        DeliveryDate = deliveryDate;
    }

    private static void Validate(CargoType type, Guid driverId, Guid vehicleId, Weight vehicleWeightCapacity, Volume vehicleVolumeCapacity, DateOnly deliveryDate)
    {
        var errors = new List<string>();

        if (!Enum.IsDefined(typeof(CargoType), type))
            errors.Add("Invalid cargo type.");

        if (driverId == Guid.Empty)
            errors.Add("Driver id cannot be empty.");

        if (vehicleId == Guid.Empty)
            errors.Add("Vehicle id cannot be empty.");

        if (deliveryDate < DateOnly.FromDateTime(DateTime.UtcNow))
            errors.Add("Delivery date cannot be in the past.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
