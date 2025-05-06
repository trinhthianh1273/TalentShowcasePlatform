using Application.Features.Payments.Command;
using Application.Features.Payments.Dto;
using Application.Features.Payments.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
	private readonly IMediator _mediator;

	public PaymentsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreatePayment(CreatePaymentCommand command)
	{
		return await _mediator.Send(command); 
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<PaymentDto>>> GetPaymentById(Guid id)
	{
		return await _mediator.Send(new GetPaymentByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<PaymentDto>>>> GetAllPayments()
	{
		return await _mediator.Send(new GetAllPaymentsQuery()); 
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdatePayment(Guid id, UpdatePaymentCommand command)
	{
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeletePayment(Guid id)
	{
		return await _mediator.Send(new DeletePaymentCommand { Id = id }); 
	}
}
