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

public class DeleteNotificationCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}


public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteNotificationHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
	{
		var notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(request.Id);

		if (notification == null)
		{
			return Result<bool>.Failure("Notification not found.");
		}

		await _unitOfWork.Repository<Notification>().DeleteAsync(notification);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete notification.");
		}
	}
}


