using AutoMapper;
using LogisticsDeliveryManager.Application.UseCases.Drivers.CreateDriver;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsDeliveryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CreateDriverResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDriver(
        [FromServices] ICreateDriverUseCase useCase,
        [FromServices] IMapper mapper,
        [FromBody] CreateDriverRequestJson request)
    {
        if (request is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);

        var command = new CreateDriverCommand(
            request.EmployeeId,
            request.LicenseTypes.Select(l => (DriverLicenseType)l).ToList());

        var driver = await useCase.Execute(command);

        var response = mapper.Map<CreateDriverResponseJson>(driver);

        return Created(string.Empty, response);
    }
}
