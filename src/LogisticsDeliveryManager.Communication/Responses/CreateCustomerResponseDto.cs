using LogisticsDeliveryManager.Communication.Enums;
using LogisticsDeliveryManager.Communication.Requests;

namespace LogisticsDeliveryManager.Communication.Responses
{
    public class CreateCustomerResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public List<AddressRequestDto> Address { get; set; }
        public CustomerTypeDto CustomerType { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
