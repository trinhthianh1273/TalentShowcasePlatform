using Application.Features.Follows.Command;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

public class FollowsController : ControllerBase
{
	private readonly IMediator _mediator;

	public FollowsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<bool>>> FollowUser(CreateFollowCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpDelete]
	public async Task<ActionResult<Result<bool>>> UnfollowUser(DeleteFollowCommand command)
	{
		return await _mediator.Send(command);
	}
}
