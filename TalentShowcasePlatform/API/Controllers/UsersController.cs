using Application.Features.Users.Command;
using Application.Features.Users.Dto;
using Application.Features.Users.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
	private readonly IMediator _mediator;

	public UsersController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateUser(CreateUserCommand command)
	{
		return await _mediator.Send(command); 
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<UserDto>>> GetUserById(Guid id)
	{
		return await _mediator.Send(new GetUserByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<UserDto>>>> GetAllUsers()
	{
		return await _mediator.Send(new GetAllUsersQuery()); 
	}

	[HttpGet("avatar-path/{fileName}")]
	public IActionResult GetVideoPath(string fileName)
	{
		// Lấy thư mục gốc của dự án bằng cách di chuyển lên từ thư mục hiện tại
		var projectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;


		// Kết hợp đường dẫn gốc với thư mục Assets/Videos
		var imagePath = Path.Combine(projectRootPath, "Assets", "Avatars", fileName);

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

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateUser(Guid id, UpdateUserCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteUser(Guid id)
	{
		return await _mediator.Send(new DeleteUserCommand { Id = id }); 
	}
}
