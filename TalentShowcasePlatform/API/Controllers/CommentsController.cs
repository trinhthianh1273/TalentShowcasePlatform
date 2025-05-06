using Application.Features.Comments.Command;
using Application.Features.Comments.Dto;
using Application.Features.Comments.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
	private readonly IMediator _mediator;

	public CommentsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateComment(CreateCommentCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpPost]
	[Route("CreateList")]
	public async Task<ActionResult<Result<List<CommentDto>>>> CreateListComment(CreateCommentListCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<CommentDto>>> GetCommentById(Guid id)
	{
		return await _mediator.Send(new GetCommentByIdQuery { Id = id });
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<CommentDto>>>> GetAllComments()
	{
		return await _mediator.Send(new GetAllCommentsQuery());
	}

	[HttpGet("video/{videoId}")]
	public async Task<ActionResult<Result<IEnumerable<CommentDto>>>> GetCommentsByVideoId(Guid videoId)
	{
		return await _mediator.Send(new GetCommentsByVideoIdQuery { VideoId = videoId });
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateComment(Guid id, UpdateCommentCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteComment(Guid id)
	{
		return await _mediator.Send(new DeleteCommentCommand { Id = id });
	}
}

