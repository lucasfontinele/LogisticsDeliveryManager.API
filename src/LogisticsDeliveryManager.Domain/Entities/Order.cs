
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;
public class Order
{
    public long Id { get; set; }
    public Customer Customer { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsPriority { get; set; }

    public Order(Customer customer, OrderStatus status, bool isPriority)
    {
        Validate(customer, status, isPriority);
        Customer = customer;
        Status = status;
        IsPriority = isPriority;
    }

    private static void Validate(Customer customer, OrderStatus status, bool isPriority)
    {
        var errors = new List<string>();

        if (customer == null)
            errors.Add("Customer cannot be null.");

        if (!Enum.IsDefined(typeof(OrderStatus), status))
            errors.Add("Invalid order status.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
