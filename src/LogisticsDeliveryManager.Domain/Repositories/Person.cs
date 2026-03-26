namespace LogisticsDeliveryManager.Domain.Repositories
{
    public abstract class Person
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Document { get; set; }
    }
}