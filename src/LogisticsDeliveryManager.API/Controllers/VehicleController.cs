using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Application.UseCases.Vehicles.AllocateDriver;
using LogisticsDeliveryManager.Application.UseCases.Vehicles.CreateVehicle;
using LogisticsDeliveryManager.Application.UseCases.Vehicles.GetAllVehicles;
using LogisticsDeliveryManager.Application.UseCases.Vehicles.GetVehicleById;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsDeliveryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CreateVehicleResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateVehicle(
        [FromServices] ICreateVehicleUseCase useCase,
        [FromServices] AutoMapper.IMapper mapper,
        [FromBody] CreateVehicleRequestJson request)
    {
        if (request is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);

        var command = new CreateVehicleCommand(
            request.LicensePlate,
            request.Model,
            request.WeightCapacity,
            request.VolumeCapacity,
            (CompartmentType)request.CompartmentType);

        var vehicle = await useCase.Execute(command);

        var response = mapper.Map<CreateVehicleResponseJson>(vehicle);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VehicleResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllVehicles(
        [FromServices] IGetAllVehiclesUseCase useCase,
        [FromServices] AutoMapper.IMapper mapper)
    {
        var vehicles = await useCase.Execute();
        var response = mapper.Map<IEnumerable<VehicleResponseJson>>(vehicles);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VehicleResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVehicleById(
        [FromServices] IGetVehicleByIdUseCase useCase,
        [FromServices] AutoMapper.IMapper mapper,
        [FromRoute] long id)
    {
        var vehicle = await useCase.Execute(id);
        if (vehicle is null) return NotFound();
        var response = mapper.Map<VehicleResponseJson>(vehicle);
        return Ok(response);
    }

    [HttpPut("{id}/allocate-driver")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AllocateDriver(
        [FromServices] IAllocateDriverToVehicleUseCase useCase,
        [FromRoute] long id,
        [FromBody] long driverId)
    {
        var command = new AllocateDriverToVehicleCommand(id, driverId);
        await useCase.Execute(command);
        return NoContent();
    }
}
