using Application.Features.LikeCommentGroupPosts.Command;
using Application.Features.LikeCommentGroupPosts.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LikeCommentGroupPostController : ControllerBase
{
	private readonly IMediator _mediator;

	public LikeCommentGroupPostController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateLikeCommentGroupPostCommand command)
	{
		var result = await _mediator.Send(command);
		return result.Succeeded ? Ok(result) : BadRequest(result);
	}

	[HttpDelete]
	public async Task<IActionResult> Delete(Guid userId, Guid commentGroupPostId)
	{
		var result = await _mediator.Send(new DeleteLikeCommentGroupPostCommand(userId, commentGroupPostId));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var result = await _mediator.Send(new GetLikeCommentGroupPostByIdQuery(id));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpGet("user/{userId}")]
	public async Task<IActionResult> GetByUser(Guid userId)
	{
		var result = await _mediator.Send(new GetCommentLikesByUserQuery(userId));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}

	[HttpGet("comment/{commentGroupPostId}")]
	public async Task<IActionResult> GetByComment(Guid commentGroupPostId)
	{
		var result = await _mediator.Send(new GetLikesByCommentGroupPostQuery(commentGroupPostId));
		return result.Succeeded ? Ok(result) : NotFound(result);
	}
}

