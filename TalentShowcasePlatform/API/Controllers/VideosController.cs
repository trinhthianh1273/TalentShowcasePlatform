using Application.Features.Videos.Command;
using Application.Features.Videos.Dto;
using Application.Features.Videos.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
	private readonly IMediator _mediator;

	public VideosController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateVideo(CreateVideoCommand command)
	{
		return await _mediator.Send(command); 
	}

	[HttpPost]
	[Route("CreateListVideo")]
	public async Task<ActionResult<Result<List<VideoDto>>>> CreateListVideos(List<CreateVideoCommand> videos)
	{
		var command = new CreateVideoListCommand { Videos = videos };
		return await _mediator.Send(command); 
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<VideoDto>>> GetVideoById(Guid id)
	{
		return await _mediator.Send(new GetVideoByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<VideoDto>>>> GetAllVideos()
	{
		return await _mediator.Send(new GetAllVideosQuery()); 
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateVideo(Guid id, UpdateVideoCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteVideo(Guid id)
	{
		return await _mediator.Send(new DeleteVideoCommand { Id = id }); 
	}
}

