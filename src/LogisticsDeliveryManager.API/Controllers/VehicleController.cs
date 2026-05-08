using LogisticsDeliveryManager.Communication.Enums;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Application.UseCases.Vehicles.CreateVehicle;
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
}
