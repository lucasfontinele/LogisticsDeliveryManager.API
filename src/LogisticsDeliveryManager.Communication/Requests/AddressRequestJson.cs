namespace LogisticsDeliveryManager.Communication.Requests
{
    public class AddressRequestJson
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string PostalCode { get; set; }
    }
}
