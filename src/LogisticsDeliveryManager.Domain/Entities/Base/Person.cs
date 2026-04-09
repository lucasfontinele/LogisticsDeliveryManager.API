using LogisticsDeliveryManager.Domain.Entities.Base;

namespace LogisticsDeliveryManager.Domain.Entities.Base;

public abstract class Person : EntityBase
{
    public string Name { get; set; }
    public string Document { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}