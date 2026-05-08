using AutoMapper;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.AutoMapper;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponseJson>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.AssignedVehicleId, opt => opt.MapFrom(src => src.AssignedVehicle != null ? src.AssignedVehicle.Id : (Guid?)null))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.Feedback));

        CreateMap<Address, AddressRequestJson>();
    }
}
