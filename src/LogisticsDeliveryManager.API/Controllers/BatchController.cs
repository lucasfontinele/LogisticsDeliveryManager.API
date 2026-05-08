using AutoMapper;
using LogisticsDeliveryManager.Application.UseCases.Batch.CreateBatch;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsDeliveryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BatchController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CreateBatchResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBatch(
        [FromServices] ICreateBatchUseCase useCase,
        [FromServices] IMapper mapper,
        [FromBody] CreateBatchRequestJson request)
    {
        if (request is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);

        var command = mapper.Map<CreateBatchCommand>(request);
        var batch = await useCase.Execute(command);
        var response = mapper.Map<CreateBatchResponseJson>(batch);

        return Created(string.Empty, response);
    }
}
