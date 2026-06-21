using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Requests
{
    public class CreateCustomerRequestJson
    {
        public required string Name { get; set; }
        public required string Document { get; set; }
        public required List<AddressRequestJson> Addresses { get; set; }
        public CustomerTypeJson CustomerType { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
    }
}
