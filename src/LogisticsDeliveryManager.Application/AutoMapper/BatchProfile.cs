using AutoMapper;
using LogisticsDeliveryManager.Application.UseCases.Batch.CreateBatch;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using DomainBatch = LogisticsDeliveryManager.Domain.Entities.Batch;

namespace LogisticsDeliveryManager.Application.AutoMapper;

public class BatchProfile : Profile
{
    public BatchProfile()
    {
        CreateMap<CreateBatchRequestJson, CreateBatchCommand>()
            .ConstructUsing(src => new CreateBatchCommand(
                (CargoType)src.Type,
                src.VehicleId,
                src.DriverId,
                src.DeliveryDate));

        CreateMap<DomainBatch, CreateBatchResponseJson>()
            .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.Driver.Id))
            .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.Vehicle.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (Communication.Enums.CargoTypeJson)src.Type));

        CreateMap<DomainBatch, BatchResponseJson>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.BatchOrders.Select(bo => bo.Order)));
    }
}
