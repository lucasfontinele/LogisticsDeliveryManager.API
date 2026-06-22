using AutoMapper;
using LogisticsDeliveryManager.Application.UseCases.Employees.CreateEmployee;
using LogisticsDeliveryManager.Application.UseCases.Employees.GetAllDrivers;
using LogisticsDeliveryManager.Application.UseCases.Employees.GetAllEmployees;
using LogisticsDeliveryManager.Application.UseCases.Employees.GetDriverById;
using LogisticsDeliveryManager.Application.UseCases.Employees.GetEmployeeById;
using LogisticsDeliveryManager.Application.UseCases.Employees.RegisterAsDriver;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsDeliveryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CreateEmployeeResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployee(
        [FromServices] ICreateEmployeeUseCase useCase,
        [FromServices] IMapper mapper,
        [FromBody] CreateEmployeeRequestJson request)
    {
        if (request is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);

        var command = new CreateEmployeeCommand(
            request.Name,
            request.Document,
            request.PhoneNumber,
            request.Email,
            (RoleType)request.RoleType);

        var employee = await useCase.Execute(command);

        var response = mapper.Map<CreateEmployeeResponseJson>(employee);

        return Created(string.Empty, response);
    }

    [HttpPost("{id}/driver-profile")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(EmployeeResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsDriver(
        [FromRoute] Guid id,
        [FromServices] IRegisterEmployeeAsDriverUseCase useCase,
        [FromServices] IMapper mapper,
        [FromBody] RegisterEmployeeAsDriverRequestJson request)
    {
        if (request is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);

        var command = new RegisterEmployeeAsDriverCommand(
            id,
            request.LicenseTypes.Select(licenseType => (DriverLicenseType)licenseType).ToList());

        var employee = await useCase.Execute(command);
        var response = mapper.Map<EmployeeResponseJson>(employee);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllEmployees(
        [FromServices] IGetAllEmployeesUseCase useCase,
        [FromServices] IMapper mapper)
    {
        var employees = await useCase.Execute();
        var response = mapper.Map<IEnumerable<EmployeeResponseJson>>(employees);
        return Ok(response);
    }

    [HttpGet("drivers")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDrivers(
        [FromServices] IGetAllDriversUseCase useCase,
        [FromServices] IMapper mapper)
    {
        var drivers = await useCase.Execute();
        var response = mapper.Map<IEnumerable<EmployeeResponseJson>>(drivers);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmployeeResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeeById(
        [FromServices] IGetEmployeeByIdUseCase useCase,
        [FromServices] IMapper mapper,
        [FromRoute] Guid id)
    {
        var employee = await useCase.Execute(id);
        if (employee is null) return NotFound();
        var response = mapper.Map<EmployeeResponseJson>(employee);
        return Ok(response);
    }

    [HttpGet("drivers/{id}")]
    [ProducesResponseType(typeof(EmployeeResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDriverById(
        [FromServices] IGetDriverByIdUseCase useCase,
        [FromServices] IMapper mapper,
        [FromRoute] Guid id)
    {
        var driver = await useCase.Execute(id);
        if (driver is null) return NotFound();

        var response = mapper.Map<EmployeeResponseJson>(driver);
        return Ok(response);
    }
}
