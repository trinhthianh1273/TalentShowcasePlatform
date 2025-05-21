using Application.Features.LikeGroupPosts.Command;
using Application.Features.LikeGroupPosts.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LikeGroupPostController : ControllerBase
{
	private readonly IMediator _mediator;

	public LikeGroupPostController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateLikeGroupPostCommand command)
	{
		var result = await _mediator.Send(command);
		return result.Succeeded ? Ok(result) : BadRequest(result);
	}

	[HttpDelete]
	public async Task<IActionResult> Delete(Guid userId, Guid groupPostId)
	{
		var result = await _mediator.Send(new DeleteLikeGroupPostCommand(userId, groupPostId));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _mediator.Send(new GetLikeGroupPostByIdQuery(id));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpGet("user/{userId}")]
	public async Task<IActionResult> GetByUser(Guid userId)
	{
		var result = await _mediator.Send(new GetLikesByUserQuery(userId));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpGet("group-post/{groupPostId}")]
	public async Task<IActionResult> GetByGroupPost(Guid groupPostId)
	{
		var result = await _mediator.Send(new GetLikesByGroupPostQuery(groupPostId));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}
}

