using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class BatchOrder
{
    public long Id { get; set; }
    public Batch Batch { get; set; }
    public Order Order { get; set; }
    public DateTime CreatedAt { get; set; }

    private BatchOrder() { }

    public BatchOrder(Batch batch, Order order)
    {
        Validate(batch, order);

        Batch = batch;
        Order = order;
        CreatedAt = DateTime.UtcNow;
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
   
