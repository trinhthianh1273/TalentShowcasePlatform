using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObject;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.Command;

public class UpdateNotificationCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Message { get; set; }
	public string Type { get; set; }
	public Guid? RelatedEntityId { get; set; }
	public RelatedEntityType RelatedEntityType { get; set; }
}


public class UpdateNotificationHandler : IRequestHandler<UpdateNotificationCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateNotificationHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
	{
		var notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(request.Id);

		if (notification == null)
		{
			return Result<bool>.Failure("Notification not found.");
		}

		notification.Title = request.Title;
		notification.Message = request.Message;
		notification.Type = request.Type;
		notification.RelatedEntityId = request.RelatedEntityId;
		notification.RelatedEntityType = request.RelatedEntityType;

		await _unitOfWork.Repository<Notification>().UpdateAsync(notification);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to update notification.");
		}
	}
}
