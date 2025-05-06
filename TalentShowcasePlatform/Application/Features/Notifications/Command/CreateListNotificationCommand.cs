using Application.Features.Notifications.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Command;

public class CreateListNotificationCommand : IRequest<Result<List<NotificationDto>>>
{
	public List<CreateNotificationCommand> Notifications { get; set; }
}


public class CreateListNotificationCommandHandler : IRequestHandler<CreateListNotificationCommand, Result<List<NotificationDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateListNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<NotificationDto>>> Handle(CreateListNotificationCommand request, CancellationToken cancellationToken)
	{
		var notifications = new List<Notification>();
		foreach (var item in request.Notifications)
		{
			var notification = new Notification
			{
				Id = Guid.NewGuid(),
				UserId = item.UserId,
				Message = item.Message,
				IsRead = item.IsRead,
				CreatedAt = DateTime.UtcNow
			};
			notifications.Add(notification);
		}
		
		try
		{

			await _unitOfWork.Repository<Notification>().AddRangeAsync(notifications);
			var saveResult = await _unitOfWork.Save(cancellationToken);

			if (saveResult > 0)
			{
				var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);
				return Result<List<NotificationDto>>.Success(notificationDtos, "Notificated successfully");
			}
			else
			{
				return Result<List<NotificationDto>>.Failure();
			}
		} catch(Exception ex)
		{
			// Handle exception
			return Result<List<NotificationDto>>.Failure(ex.Message);
		}
	}
}