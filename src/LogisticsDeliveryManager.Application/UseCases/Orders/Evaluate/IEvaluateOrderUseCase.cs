namespace LogisticsDeliveryManager.Application.UseCases.Orders.Evaluate;

public class EvaluateOrderCommand
{
    public Guid OrderId { get; set; }
    public int Rating { get; set; }
    public string? Feedback { get; set; }

    public EvaluateOrderCommand(Guid orderId, int rating, string? feedback)
    {
        OrderId = orderId;
        Rating = rating;
        Feedback = feedback;
    }
}

public interface IEvaluateOrderUseCase
{
    Task Execute(EvaluateOrderCommand command);
}
