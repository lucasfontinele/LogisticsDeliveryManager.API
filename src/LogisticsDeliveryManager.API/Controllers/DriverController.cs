using AutoMapper;
using LogisticsDeliveryManager.Application.UseCases.Drivers.CreateDriver;
using LogisticsDeliveryManager.Application.UseCases.Drivers.GetAllDrivers;
using LogisticsDeliveryManager.Application.UseCases.Drivers.GetDriverById;
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

        var driverEmployee = await useCase.Execute(command);

        var response = mapper.Map<CreateDriverResponseJson>(driverEmployee);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DriverResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDrivers(
        [FromServices] IGetAllDriversUseCase useCase,
        [FromServices] IMapper mapper)
    {
        var drivers = await useCase.Execute();
        var response = mapper.Map<IEnumerable<DriverResponseJson>>(drivers);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DriverResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDriverById(
        [FromServices] IGetDriverByIdUseCase useCase,
        [FromServices] IMapper mapper,
        [FromRoute] Guid id)
    {
        var driver = await useCase.Execute(id);
        if (driver is null) return NotFound();
        var response = mapper.Map<DriverResponseJson>(driver);
        return Ok(response);
    }
}
