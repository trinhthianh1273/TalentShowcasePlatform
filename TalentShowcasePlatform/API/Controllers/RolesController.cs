using Application.Features.Roles.Command;
using Application.Features.Roles.Dto;
using Application.Features.Roles.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
	private readonly IMediator _mediator;

	public RolesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateRole(CreateRoleCommand command)
	{
		return await _mediator.Send(command); 
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<RoleDto>>> GetRoleById(Guid id)
	{
		return await _mediator.Send(new GetRoleByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<RoleDto>>>> GetAllRoles()
	{
		return await _mediator.Send(new GetAllRolesQuery()); 
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateRole(Guid id, UpdateRoleCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteRole(Guid id)
	{
		return await _mediator.Send(new DeleteRoleCommand { Id = id }); 
	}
}

