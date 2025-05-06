using Application.Features.Auth.Dto;
using Application.Features.Auth.LoginUser;
using Application.Features.Auth.RegisterUser;
using Application.Features.Users.Query;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[ApiController]
[Route("api/auth")] // Separate controller for auth
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IConfiguration _configuration;

	public AuthController(IMediator mediator, IConfiguration configuration)
	{
		_mediator = mediator;
		_configuration = configuration;
	}

	[HttpPost("register")]
	public async Task<ActionResult<Result<AuthenticationResponseDto>>> Register(RegisterRequestDto request)
	{
		var command = new RegisterUserCommand
		{
			UserName = request.UserName,
			Email = request.Email,
			Password = request.Password,
			RoleId = request.RoleId
		};

		return await _mediator.Send(command);
	}

	[HttpPost("login")]
	public async Task<ActionResult<Result<AuthenticationResponseDto>>> Login(LoginRequestDto request)
	{
		var command = new LoginUserCommand
		{
			Email = request.Email,
			Password = request.Password
		};

		var result = await _mediator.Send(command);

		if (result.Succeeded)
		{
			return result;
		}
		else
		{
			return Unauthorized(result); // Or BadRequest, depending on your API design
		}
	}

}