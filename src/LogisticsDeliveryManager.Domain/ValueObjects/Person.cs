namespace LogisticsDeliveryManager.Domain.ValueObjects;

public abstract class Person
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
}