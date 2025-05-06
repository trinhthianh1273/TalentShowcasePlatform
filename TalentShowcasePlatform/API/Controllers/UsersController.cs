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
