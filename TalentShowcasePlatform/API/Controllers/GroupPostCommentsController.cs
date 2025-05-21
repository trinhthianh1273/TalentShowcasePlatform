using Application.Features.GroupPostComments.Command;
using Application.Features.GroupPostComments.Dto;
using Application.Features.GroupPostComments.Query;
using Application.Features.GroupPosts.Command;
using Application.Features.GroupPosts.Dto;
using Application.Features.GroupPosts.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupPostCommentsController : ControllerBase
{
	private readonly IMediator _mediator;

	public GroupPostCommentsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<GroupPostCommentDto>>>> GetAllGroupPostComments()
	{
		var result = await _mediator.Send(new GetAllCommentPostQuery());
		if (result.Succeeded)
		{
			return Ok(result);
		}
		else
		{
			return NotFound(result);

		}
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<GroupPostCommentDto>>> GetGroupPostCommentById(Guid id)
	{
		return await _mediator.Send(new GetCommentPostByIdQuery { Id = id });
	}

	[HttpGet("by-post/{postId}")] // Hoặc một route phù hợp khác
	public async Task<ActionResult<Result<IEnumerable<GroupPostCommentDto>>>> GetGroupPostCommentsByPost(Guid postId)
	{
		var result = await _mediator.Send(new GetCommentByPostQuery(postId));
		if (result.Succeeded)
		{
			return Ok(result);
		}
		else
		{
			return NotFound(result); // Hoặc mã trạng thái phù hợp khác
		}
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateGroupPostComment([FromBody] CreateGroupPostCommentCommand command)
	{
		var result = await _mediator.Send(command);

		if (result.Succeeded)
		{
			// Trả về CreatedAtAction với ID của resource mới tạo
			return Ok(result);
		}
		else
		{
			return BadRequest(result);
		}
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteGroupPostComment(Guid id)
	{
		var command = new DeleteGroupPostCommentCommand { Id = id };
		var result = await _mediator.Send(command);

		if (result.Succeeded)
		{
			return Ok(result); // Hoặc có thể trả về Ok(result) nếu bạn muốn trả về true
		}
		else
		{
			return BadRequest(result);
		}
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateGroupPostComment(Guid id, [FromBody] UpdateGroupPostCommentCommand command)
	{
		if (id != command.Id)
		{
			return BadRequest(Result<bool>.Failure("Id không khớp."));
		}

		var result = await _mediator.Send(command);

		if (result.Succeeded)
		{
			return Ok(result); // Hoặc có thể trả về Ok(result) nếu bạn muốn trả về true
		}
		else
		{
			return BadRequest(result);
		}
	}
}
