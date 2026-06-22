using AutoMapper;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.AutoMapper;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, CreateEmployeeResponseJson>()
            .ForMember(dest => dest.RoleType, opt => opt.MapFrom(src => (int)src.RoleType))
            .ForMember(dest => dest.LicenseTypes, opt => opt.MapFrom(src => src.LicenseTypes.Select(l => (int)l)));

        CreateMap<Employee, EmployeeResponseJson>()
            .ForMember(dest => dest.RoleType, opt => opt.MapFrom(src => (int)src.RoleType))
            .ForMember(dest => dest.LicenseTypes, opt => opt.MapFrom(src => src.LicenseTypes.Select(l => (int)l)));
    }
}
