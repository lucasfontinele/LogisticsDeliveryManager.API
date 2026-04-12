using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Requests
{
    public class CreateCustomerRequestJson
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public List<AddressRequestJson> Addresses { get; set; }
        public CustomerTypeJson CustomerType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
