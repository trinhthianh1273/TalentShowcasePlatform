using Application.Features.Videos.Command;
using Application.Features.Videos.Dto;
using Application.Features.Videos.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[RequestSizeLimit(104857600)] // 100MB
[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
	private readonly IMediator _mediator;

	public VideosController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("video-path/{fileName}")]
	public IActionResult GetVideoPath(string fileName)
	{
		// Lấy thư mục gốc của dự án bằng cách di chuyển lên từ thư mục hiện tại
		var projectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;


		// Kết hợp đường dẫn gốc với thư mục Assets/Videos
		var videoPath = Path.Combine(projectRootPath, "Assets", "Videos", fileName);

		Console.WriteLine(videoPath);
		if (!System.IO.File.Exists(videoPath))
			return NotFound("Video not found" + videoPath);

		var stream = new FileStream(videoPath, FileMode.Open, FileAccess.Read);
		return File(stream, "video/mp4", enableRangeProcessing: true); // hỗ trợ seek
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateVideo([FromForm] CreateVideoCommand command)
	{
		var result = await _mediator.Send(command);
		if (result.Succeeded)
			return Ok(result);
		return BadRequest(result); 
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

	[HttpGet("User/{id}")]
	public async Task<ActionResult<Result<IEnumerable<VideoDto>>>> GetVideoByUserId(Guid id)
	{
		return await _mediator.Send(new GetVideoByUserIdQuery { Id = id });
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<VideoDto>>>> GetAllVideos()
	{
		return await _mediator.Send(new GetAllVideosQuery()); 
	}

	[HttpGet]
	[Route("page")]
	public async Task<ActionResult<PaginatedResult<VideoDto>>> GetVideoPagination([FromQuery] GetVideoPaginationQuery query)
	{
		return await _mediator.Send(query);
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

