namespace LogisticsDeliveryManager.Domain.Entities
{
    public class Address
    {
        public long Id { get; set; }
        public String Street { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String PostalCode { get; set; }
    }
}