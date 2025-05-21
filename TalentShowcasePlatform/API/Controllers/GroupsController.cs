using Application.Features.Groups.Command;
using Application.Features.Groups.Dto;
using Application.Features.Groups.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
	private readonly IMediator _mediator;

	public GroupsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateGroup([FromForm] CreateGroupCommand command)
	{
		var result = await _mediator.Send(command); 
		if(result.Succeeded)
		{
			return Ok(result);
		}
		else
		{
			return BadRequest(result); // Hoặc mã trạng thái phù hợp khác
		}
	}

	[HttpPost]
	[Route("CreateList")]
	public async Task<ActionResult<Result<List<GroupDto>>>> CreateListGroup(CreateListGroupCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<GroupDto>>> GetGroupById(Guid id)
	{
		return await _mediator.Send(new GetGroupByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<GroupDto>>>> GetAllGroups()
	{
		return await _mediator.Send(new GetAllGroupostsQuery()); 
	}

	[HttpGet("createdby/{createdBy}")]
	public async Task<ActionResult<Result<IEnumerable<GroupDto>>>> GetGroupsCreatedByUser(Guid createdBy)
	{
		return await _mediator.Send(new GetGroupsCreatedByUserQuery { CreatedBy = createdBy }); 
	}

	[HttpGet("joined-group/{id}")]
	public async Task<ActionResult<Result<IEnumerable<GroupDto>>>> GetJoinedGroupByUserId(Guid id)
	{
		return await _mediator.Send(new GetJoinedGroupForUser(id));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateGroup(Guid id, UpdateGroupCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteGroup(Guid id)
	{
		return await _mediator.Send(new DeleteGroupCommand { Id = id }); 
	}

	[HttpGet("avatar-path/{fileName}")]
	public IActionResult GetVideoPath(string fileName)
	{
		// Lấy thư mục gốc của dự án bằng cách di chuyển lên từ thư mục hiện tại
		var projectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;


		// Kết hợp đường dẫn gốc với thư mục Assets/Videos
		var imagePath = Path.Combine(projectRootPath, "Assets", "GroupAvatar", fileName);

		Console.WriteLine(imagePath);
		if (!System.IO.File.Exists(imagePath))
			return NotFound("Image not found: " + imagePath);

		var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
		// Xác định Content-Type dựa trên đuôi file để trình duyệt hiển thị đúng loại ảnh
		var contentType = GetContentType(fileName);
		return File(stream, contentType);
	}

	private string GetContentType(string fileName)
	{
		var extension = Path.GetExtension(fileName).ToLowerInvariant();

		switch (extension)
		{
			case ".jpg":
			case ".jpeg":
				return "image/jpeg";
			case ".png":
				return "image/png";
			case ".gif":
				return "image/gif";
			case ".bmp":
				return "image/bmp";
			// Thêm các định dạng ảnh khác nếu cần
			default:
				return "application/octet-stream"; // Default nếu không nhận diện được
		}
	}
}


