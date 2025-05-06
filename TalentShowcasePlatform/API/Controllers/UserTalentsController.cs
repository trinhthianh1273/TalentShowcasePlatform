using Application.Features.UserTalents.Command;
using Application.Features.UserTalents.Dto;
using Application.Features.UserTalents.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTalentsController : ControllerBase
{
	private readonly IMediator _mediator;

	public UserTalentsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateUserTalent(CreateUserTalentCommand command)
	{
		return await _mediator.Send(command); 
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<UserTalentDto>>> GetUserTalentById(Guid id)
	{
		return await _mediator.Send(new GetUserTalentByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<UserTalentDto>>>> GetAllUserTalents()
	{
		return await _mediator.Send(new GetAllUserTalentsQuery());
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateUserTalent(Guid id, UpdateUserTalentCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteUserTalent(Guid id)
	{
		return await _mediator.Send(new DeleteUserTalentCommand { Id = id }); 
	}
}

