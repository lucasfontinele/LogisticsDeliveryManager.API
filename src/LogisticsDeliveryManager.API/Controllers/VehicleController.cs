using LogisticsDeliveryManager.Application.UseCases.Vehicle.CreateVehicle;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using Microsoft.AspNetCore.Http;
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
        [FromBody] CreateVehicleRequestJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
