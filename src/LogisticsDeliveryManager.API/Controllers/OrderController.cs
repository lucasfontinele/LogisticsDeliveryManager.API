using AutoMapper;
using LogisticsDeliveryManager.Application.UseCases.Orders.CreateOrder;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Repositories.Employees;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using LogisticsDeliveryManager.Application.UseCases.Orders.GetAllOrders;
using LogisticsDeliveryManager.Application.UseCases.Orders.GetOrderById;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsDeliveryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(
        [FromServices] ICreateOrderUseCase useCase,
        [FromServices] IMapper mapper,
        [FromBody] CreateOrderRequestJson request)
    {
        if (request is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);

        var command = new CreateOrderCommand(
            request.CustomerId,
            new CreateOrderAddressCommand(
                request.DestinationAddress.Street,
                request.DestinationAddress.City,
                request.DestinationAddress.State,
                request.DestinationAddress.PostalCode),
            (CargoType)request.CargoType,
            request.Weight,
            request.Volume,
            request.IsPriority);

        var order = await useCase.Execute(command);

        var response = mapper.Map<OrderResponseJson>(order);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllOrders(
        [FromServices] IGetAllOrdersUseCase useCase,
        [FromServices] IMapper mapper)
    {
        var orders = await useCase.Execute();
        var response = mapper.Map<IEnumerable<OrderResponseJson>>(orders);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(
        [FromServices] IGetOrderByIdUseCase useCase,
        [FromServices] IMapper mapper,
        [FromRoute] Guid id)
    {
        var order = await useCase.Execute(id);
        if (order is null) return NotFound();
        var response = mapper.Map<OrderResponseJson>(order);
        return Ok(response);
    }

    [HttpGet("customer/{customerId}")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerOrders(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.GetCustomerOrders.IGetCustomerOrdersUseCase useCase,
        [FromServices] IMapper mapper,
        [FromRoute] Guid customerId)
    {
        var orders = await useCase.Execute(customerId);
        var response = mapper.Map<IEnumerable<OrderResponseJson>>(orders);
        return Ok(response);
    }

    [HttpGet("driver/{driverId}")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDriverOrders(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.GetDriverOrders.IGetDriverOrdersUseCase useCase,
        [FromServices] IMapper mapper,
        [FromRoute] Guid driverId)
    {
        var orders = await useCase.Execute(driverId);
        var response = mapper.Map<IEnumerable<OrderResponseJson>>(orders);
        return Ok(response);
    }

    [HttpGet("employee/{employeeId}")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeOrders(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.GetDriverOrders.IGetDriverOrdersUseCase useCase,
        [FromServices] IEmployeeRepository employeeRepository,
        [FromServices] IMapper mapper,
        [FromRoute] Guid employeeId)
    {
        var employee = await employeeRepository.GetById(employeeId);
        if (employee == null || employee.RoleType != Domain.Enums.RoleType.Driver) return Ok(Enumerable.Empty<OrderResponseJson>());

        var orders = await useCase.Execute(employee.Id);
        var response = mapper.Map<IEnumerable<OrderResponseJson>>(orders);
        return Ok(response);
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateStatus(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.UpdateStatus.IUpdateOrderStatusUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] OrderStatus status)
    {
        var command = new LogisticsDeliveryManager.Application.UseCases.Orders.UpdateStatus.UpdateOrderStatusCommand(id, status);
        await useCase.Execute(command);
        return NoContent();
    }

    [HttpPost("{id}/proof")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UploadProof(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.UploadProof.IUploadOrderProofUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] string base64Image)
    {
        var command = new LogisticsDeliveryManager.Application.UseCases.Orders.UploadProof.UploadOrderProofCommand(id, base64Image);
        await useCase.Execute(command);
        return NoContent();
    }

    [HttpPost("{id}/evaluate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Evaluate(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.Evaluate.IEvaluateOrderUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] EvaluateRequestJson request)
    {
        var command = new LogisticsDeliveryManager.Application.UseCases.Orders.Evaluate.EvaluateOrderCommand(id, request.Rating, request.Feedback);
        await useCase.Execute(command);
        return NoContent();
    }

    [HttpPatch("{id}/assign-vehicle/{vehicleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AssignVehicle(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.AssignVehicle.IAssignOrderToVehicleUseCase useCase,
        [FromRoute] Guid id,
        [FromRoute] Guid vehicleId)
    {
        await useCase.Execute(id, vehicleId);
        return NoContent();
    }
}
