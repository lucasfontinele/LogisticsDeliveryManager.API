using AutoMapper;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.AutoMapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CreateCustomerResponseJson>()
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => src.CustomerType));

        CreateMap<Address, AddressRequestJson>();
    }
}
