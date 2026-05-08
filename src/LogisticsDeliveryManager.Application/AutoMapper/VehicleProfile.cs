using AutoMapper;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.AutoMapper;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<Vehicle, CreateVehicleResponseJson>()
            .ForMember(dest => dest.CompartmentType, opt => opt.MapFrom(src => (int)src.CompartmentType));

        CreateMap<Vehicle, VehicleResponseJson>()
            .ForMember(dest => dest.CompartmentType, opt => opt.MapFrom(src => (int)src.CompartmentType));
    }
}
