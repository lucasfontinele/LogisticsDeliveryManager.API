using AutoMapper;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.AutoMapper;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        CreateMap<Employee, CreateDriverResponseJson>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.LicenseTypes, opt => opt.MapFrom(src => src.LicenseTypes.Select(l => (int)l)));

        CreateMap<Employee, DriverResponseJson>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.LicenseTypes, opt => opt.MapFrom(src => src.LicenseTypes.Select(l => (int)l)));
    }
}
