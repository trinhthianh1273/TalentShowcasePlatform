using Infrastructure.Persistences.BEContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthCheckController : ControllerBase
{
	private readonly BEContext _context;

	public HealthCheckController(BEContext context)
	{
		_context = context;
	}

	[HttpGet("Database")]
	public IActionResult CheckDatabaseConnection()
	{
		try
		{
			// Chỉ kiểm tra kết nối database
			_context.Database.CanConnect();

			return Ok("Database is connected and accessible.");
		}
		catch (Exception ex)
		{
			return StatusCode((int)HttpStatusCode.InternalServerError, $"Database connection failed: {ex.Message}");
		}
	}

	[HttpGet("Status")]
	public IActionResult GetStatus()
	{
		return Ok("Service is running.");
	}
}
