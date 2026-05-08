using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class BatchOrder : EntityBase
{
    public Batch Batch { get; private set; }
    public Order Order { get; private set; }

    private BatchOrder() { }

    public BatchOrder(Batch batch, Order order)
    {
        Validate(batch, order);

        Batch = batch;
        Order = order;
    }

    private static void Validate(Batch batch, Order order)
    {
        var errors = new List<string>();

        if (batch == null)
            errors.Add("Batch cannot be null.");

        if (order == null)
            errors.Add("Order cannot be null.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
   
