using Application.Features.GroupMembers.Command;
using Application.Features.GroupMembers.Dto;
using Application.Features.GroupMembers.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupMembersController : ControllerBase
{
	private readonly IMediator _mediator;

	public GroupMembersController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> AddUserToGroup(AddUserToGroupCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpPost]
	[Route("CreateList")]
	public async Task<ActionResult<Result<List<GroupMemberDto>>>> AddListUserToGroup(AddListUserToGroupCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Result<GroupMemberDto>>> GetGroupMemberById(Guid id)
	{
		return await _mediator.Send(new GetGroupMemberByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<GroupMemberDto>>>> GetAllGroupMembers()
	{
		return await _mediator.Send(new GetAllGroupMembersQuery()); 
	}

	[HttpGet("group/{groupId}")]
	public async Task<ActionResult<Result<IEnumerable<GroupMemberDto>>>> GetGroupMembersByGroupId(Guid groupId)
	{
		return await _mediator.Send(new GetGroupMembersByGroupIdQuery { GroupId = groupId }); 
	}

	[HttpGet("user/{userId}")]
	public async Task<ActionResult<Result<IEnumerable<GroupMemberDto>>>> GetGroupMembersByUserId(Guid userId)
	{
		return await _mediator.Send(new GetGroupMembersByUserIdQuery { UserId = userId }); 
	}

	[HttpDelete("group/{groupId}/user/{userId}")]
	public async Task<ActionResult<Result<bool>>> RemoveUserFromGroup(Guid groupId, Guid userId)
	{
		var command = new RemoveUserFromGroupCommand { GroupId = groupId, UserId = userId };
		return await _mediator.Send(command);
	}
}