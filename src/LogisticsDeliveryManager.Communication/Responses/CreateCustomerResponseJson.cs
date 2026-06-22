using LogisticsDeliveryManager.Communication.Enums;
using LogisticsDeliveryManager.Communication.Requests;

namespace LogisticsDeliveryManager.Communication.Responses
{
    public class CreateCustomerResponseJson
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Document { get; set; }
        public required List<AddressRequestJson> Addresses { get; set; }
        public CustomerTypeJson CustomerType { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
    }
}
