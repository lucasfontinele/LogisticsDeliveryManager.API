using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities
{
    public class Customer
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public CustomerType CustomerType { get; set; }
    }
}