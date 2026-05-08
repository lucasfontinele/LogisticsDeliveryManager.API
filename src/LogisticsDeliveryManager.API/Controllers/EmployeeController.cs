using AutoMapper;
using LogisticsDeliveryManager.Application.UseCases.Employees.CreateEmployee;
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
}
