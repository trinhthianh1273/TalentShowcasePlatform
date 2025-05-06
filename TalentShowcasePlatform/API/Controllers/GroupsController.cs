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
	public async Task<ActionResult<Result<Guid>>> CreateGroup(CreateGroupCommand command)
	{
		return await _mediator.Send(command); 
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
		return await _mediator.Send(new GetAllGroupsQuery()); 
	}

	[HttpGet("createdby/{createdBy}")]
	public async Task<ActionResult<Result<IEnumerable<GroupDto>>>> GetGroupsCreatedByUser(Guid createdBy)
	{
		return await _mediator.Send(new GetGroupsCreatedByUserQuery { CreatedBy = createdBy }); 
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
}


