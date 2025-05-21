using Application.Features.GroupPosts.Command;
using Application.Features.GroupPosts.Dto;
using Application.Features.GroupPosts.Query;
using Application.Features.Groups.Dto;
using Application.Features.Groups.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupPostsController : ControllerBase
{
	private readonly IMediator _mediator;

	public GroupPostsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<GroupPostDto>>>> GetAllGroupPosts()
	{
		var result = await _mediator.Send(new GetAllCommentByGroupQuery());
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
	public async Task<ActionResult<Result<GroupPostDto>>> GetGroupPostById(Guid id)
	{
		return await _mediator.Send(new GetCommentByIdQuery { Id = id });
	}

	[HttpGet("by-group/{groupId}")] // Hoặc một route phù hợp khác
	public async Task<ActionResult<Result<IEnumerable<GroupPostDto>>>> GetAllGroupPostsByGroup(Guid groupId)
	{
		var result = await _mediator.Send(new GetAllGroupPostsByGroupQuery(groupId));
		if (result.Succeeded)
		{
			return Ok(result);
		}
		else
		{
			return NotFound(result); // Hoặc mã trạng thái phù hợp khác
		}
	}

	[HttpGet("by-user-group/{userId}/{groupId}")] // Hoặc một route phù hợp khác
	public async Task<ActionResult<Result<IEnumerable<GroupPostDto>>>> GetAllGroupPostsByUserGroup(Guid userId, Guid groupId)
	{
		var result = await _mediator.Send(new GetAllGroupPostsByUserGroupQuery(userId, groupId));
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
	public async Task<ActionResult<Result<Guid>>> CreateGroupPost([FromBody] CreateGroupPostCommand command)
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
	public async Task<ActionResult<Result<bool>>> DeleteGroupPost(Guid id)
	{
		var command = new DeleteGroupPostCommand { Id = id };
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
	public async Task<ActionResult<Result<bool>>> UpdateGroupPost(Guid id, [FromBody] UpdateGroupPostCommand command)
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

	[HttpGet("avatar-path/{fileName}")]
	public IActionResult GetVideoPath(string fileName)
	{
		// Lấy thư mục gốc của dự án bằng cách di chuyển lên từ thư mục hiện tại
		var projectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;


		// Kết hợp đường dẫn gốc với thư mục Assets/Videos
		var imagePath = Path.Combine(projectRootPath, "Assets", "GroupPosts", fileName);

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
