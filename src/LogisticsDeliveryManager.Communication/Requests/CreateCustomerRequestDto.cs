using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Requests
{
    public class CreateCustomerRequestDto
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public List<AddressRequestDto> Addresses { get; set; }
        public CustomerTypeDto CustomerType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
