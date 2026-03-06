using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public Customer Customer { get; set; }
        public Shipping Shipping { get; set; }
        public ICollection<Cargo> Cargo { get; set; }
        public OrderStatus Status { get; set; }
    }
}

