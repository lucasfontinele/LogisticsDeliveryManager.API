using AutoMapper;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Entities;

namespace LogisticsDeliveryManager.Application.AutoMapper;

public class DriverProfile : Profile
{
    public DriverProfile()
    {
        CreateMap<Driver, CreateDriverResponseJson>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employee.Id))
            .ForMember(dest => dest.LicenseTypes, opt => opt.MapFrom(src => src.LicenseTypes.Select(l => (int)l)));
    }
}
