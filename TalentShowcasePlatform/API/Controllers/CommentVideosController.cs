using Application.Features.CommentVideos.Command;
using Application.Features.CommentVideos.Dto;
using Application.Features.CommentVideos.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentVideosController : ControllerBase
{
	private readonly IMediator _mediator;

	public CommentVideosController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateComment(CreateCommentVideoCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpPost]
	[Route("CreateList")]
	public async Task<ActionResult<Result<List<CommentVideoDto>>>> CreateListComment(CreateCommentVideoListCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<CommentVideoDto>>> GetCommentById(Guid id)
	{
		return await _mediator.Send(new GetCommentVideoByIdQuery { Id = id });
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<CommentVideoDto>>>> GetAllCommentVideos()
	{
		return await _mediator.Send(new GetAllCommentVideosQuery());
	}

	[HttpGet("video/{videoId}")]
	public async Task<ActionResult<Result<IEnumerable<CommentVideoDto>>>> GetCommentVideosByVideoId(Guid videoId)
	{
		return await _mediator.Send(new GetCommentVideosByVideoIdQuery { VideoId = videoId });
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateComment(Guid id, UpdateCommentVideoCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteComment(Guid id)
	{
		return await _mediator.Send(new DeleteCommentVideoCommand { Id = id });
	}
}

