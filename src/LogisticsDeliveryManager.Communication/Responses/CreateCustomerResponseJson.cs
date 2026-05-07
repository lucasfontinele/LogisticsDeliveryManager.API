using LogisticsDeliveryManager.Communication.Enums;
using LogisticsDeliveryManager.Communication.Requests;

namespace LogisticsDeliveryManager.Communication.Responses
{
    public class CreateCustomerResponseJson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public List<AddressRequestJson> Addresses { get; set; }
        public CustomerTypeJson CustomerType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
