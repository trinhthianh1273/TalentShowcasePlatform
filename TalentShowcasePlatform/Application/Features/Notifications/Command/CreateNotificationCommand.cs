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

public class CreateNotificationCommand : IRequest<Result<Guid>>
{
	public Guid UserId { get; set; }
	public string Message { get; set; }
	public bool IsRead { get; set; }
}


public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateNotificationHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
	{
		var notification = new Notification
		{
			Id = Guid.NewGuid(),
			UserId = request.UserId,
			Message = request.Message,
			IsRead = request.IsRead,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<Notification>().AddAsync(notification);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(notification.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create notification.");
		}
	}
}