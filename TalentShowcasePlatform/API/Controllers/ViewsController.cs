using Application.Features.Views.Command;
using Application.Features.Views.Dto;
using Application.Features.Views.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ViewsController : ControllerBase
{
	private readonly IMediator _mediator;

	public ViewsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateView(CreateViewCommand command)
	{
		return await _mediator.Send(command); 
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<ViewDto>>> GetViewById(Guid id)
	{
		return await _mediator.Send(new GetViewByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<ViewDto>>>> GetAllViews()
	{
		return await _mediator.Send(new GetAllViewsQuery()); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteView(Guid id)
	{
		return await _mediator.Send(new DeleteViewCommand { Id = id }); 
	}
}
