using Application.Features.Notifications.Command;
using Application.Features.Notifications.Dto;
using Application.Features.Notifications.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
	private readonly IMediator _mediator;

	public NotificationsController(IMediator mediator)
	{
		_mediator = mediator;
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<Result<NotificationDto>>> GetNotificationById(Guid id)
	{
		return await _mediator.Send(new GetNotificationByIdQuery { Id = id }); 
	}

	[HttpGet]
	public async Task<ActionResult<Result<IEnumerable<NotificationDto>>>> GetAllNotifications()
	{
		return await _mediator.Send(new GetAllNotificationsQuery()); 
	}

	[HttpGet("user/{id}")]
	public async Task<ActionResult<Result<IEnumerable<NotificationDto>>>> GetNotificationByUser(Guid id)
	{
		return await _mediator.Send(new GetNotificationsByUserQuery(id));
	}

	[HttpPost]
	public async Task<ActionResult<Result<Guid>>> CreateNotification(CreateNotificationCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpPut("{id}")]
	public async Task<ActionResult<Result<bool>>> UpdateNotification(Guid id, UpdateNotificationCommand command)
	{ 
		return await _mediator.Send(command); 
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<Result<bool>>> DeleteNotification(Guid id)
	{
		return await _mediator.Send(new DeleteNotificationCommand { Id = id }); 
	}
}

