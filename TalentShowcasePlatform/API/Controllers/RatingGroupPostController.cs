using Application.Features.RatingGroupPosts.Command;
using Application.Features.RatingGroupPosts.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingGroupPostController : ControllerBase
{
	private readonly IMediator _mediator;

	public RatingGroupPostController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateRatingGroupPostCommand command)
	{
		var result = await _mediator.Send(command);
		return result.Succeeded ? Ok(result) : BadRequest(result);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(Guid id, [FromBody] int value)
	{
		var result = await _mediator.Send(new UpdateRatingGroupPostCommand(id, value));
		return result.Succeeded ? Ok(result) : BadRequest(result);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var result = await _mediator.Send(new DeleteRatingGroupPostCommand(id));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _mediator.Send(new GetRatingGroupPostByIdQuery(id));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpGet("group-post/{groupPostId}")]
	public async Task<IActionResult> GetByGroupPost(Guid groupPostId)
	{
		var result = await _mediator.Send(new GetRatingsByGroupPostQuery(groupPostId));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpGet("user/{userId}")]
	public async Task<IActionResult> GetByUser(Guid userId)
	{
		var result = await _mediator.Send(new GetRatingsByUserQuery(userId));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

}

