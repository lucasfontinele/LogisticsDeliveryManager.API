using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Batch : EntityBase
{
    private readonly List<BatchOrder> _batchOrders = new();

    public CargoType Type { get; private set; }
    public Driver Driver { get; private set; }
    public Vehicle Vehicle { get; private set; }
    public DateOnly DeliveryDate { get; private set; }

    public IReadOnlyCollection<BatchOrder> BatchOrders => _batchOrders;

    private Batch() { }

    public Batch(CargoType type, Driver driver, Vehicle vehicle, DateOnly deliveryDate)
    {
        Validate(type, driver, vehicle, deliveryDate);
        
        Type = type;
        Driver = driver;
        Vehicle = vehicle;
        DeliveryDate = deliveryDate;
    }

    public void AddOrder(Order order)
    {
        if (order is null)
            throw new ErrorOnValidationException(["Order cannot be null."]);

        if (_batchOrders.Any(bo =>
                (order.Id != Guid.Empty && bo.Order.Id == order.Id) ||
                ReferenceEquals(bo.Order, order)))
        {
            throw new ErrorOnValidationException(["Order is already in this batch."]);
        }

        var totalWeightAfterAdd = _batchOrders.Sum(bo => bo.Order.Weight) + order.Weight;
        if (totalWeightAfterAdd > Vehicle.WeightCapacity)
        {
            throw new ErrorOnValidationException([
                $"Batch weight capacity exceeded. Current: {_batchOrders.Sum(bo => bo.Order.Weight)}, " +
                $"Adding: {order.Weight}, Capacity: {Vehicle.WeightCapacity}."
            ]);
        }

        var totalVolumeAfterAdd = _batchOrders.Sum(bo => bo.Order.Volume) + order.Volume;
        if (totalVolumeAfterAdd > Vehicle.VolumeCapacity)
        {
            throw new ErrorOnValidationException([
                $"Batch volume capacity exceeded. Current: {_batchOrders.Sum(bo => bo.Order.Volume)}, " +
                $"Adding: {order.Volume}, Capacity: {Vehicle.VolumeCapacity}."
            ]);
        }

        _batchOrders.Add(new BatchOrder(this, order));
    }

    public void ChangeDeliveryDate(DateOnly deliveryDate)
    {
        if (deliveryDate < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ErrorOnValidationException(["Delivery date cannot be in the past."]);

        DeliveryDate = deliveryDate;
    }

    private static void Validate(CargoType type, Driver driver, Vehicle vehicle, DateOnly deliveryDate)
    {
        var errors = new List<string>();

         if (!Enum.IsDefined(typeof(CargoType), type))
            errors.Add("Invalid cargo type.");

        if (driver == null)
            errors.Add("Driver cannot be null.");

        if (vehicle == null)
            errors.Add("Vehicle cannot be null.");

        if (deliveryDate < DateOnly.FromDateTime(DateTime.UtcNow))
            errors.Add("Delivery date cannot be in the past.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
