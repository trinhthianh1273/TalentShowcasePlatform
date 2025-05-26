using Application.Features.VideoLikes.Command;
using Application.Features.VideoLikes.Dto;
using Application.Features.VideoLikes.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoLikesController : ControllerBase
{
	private readonly IMediator _mediator;

	public VideoLikesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<VideoLikeDto>>> GetVideoLikeById(Guid id)
	{
		return await _mediator.Send(new GetVideoLikeByIdQuery { Id = id }); 
	}

	[HttpGet("video/{id}")]
	public async Task<ActionResult<Result<IEnumerable<VideoLikeDto>>>> GetVideoLikeByVideoId(Guid id)
	{
		return await _mediator.Send(new GetVideoLikeByVideoIdQuery { VideoId = id });
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<VideoLikeDto>>>> GetAllVideoLikes()
	{
		return await _mediator.Send(new GetAllVideoLikesQuery()); 
	}


	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateVideoLike(CreateVideoLikeCommand command)
	{
		return await _mediator.Send(command);
	}


	[HttpPost]
	[Route("CreateList")]
	public async Task<ActionResult<Result<List<VideoLikeDto>>>> CreateListVideoLike(CreateListVideoLikeCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteVideoLike(Guid id)
	{
		return await _mediator.Send(new DeleteVideoLikeCommand { Id = id }); 
	}
}

