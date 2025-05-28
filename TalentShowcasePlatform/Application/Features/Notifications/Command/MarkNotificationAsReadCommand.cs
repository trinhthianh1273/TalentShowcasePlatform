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

public record MarkNotificationAsReadCommand(Guid NotificationId) : IRequest<Result<Guid>>;

public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public MarkNotificationAsReadCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
	{
		var repo = _unitOfWork.Repository<Notification>();
		var notification = await repo.GetByIdAsync(request.NotificationId);

		if (notification == null)
			return Result<Guid>.Failure("Notification not found");

		notification.IsRead = true;
		notification.ReadAt = DateTime.UtcNow;

		await repo.UpdateAsync(notification);
		await _unitOfWork.Save(cancellationToken);

		return Result<Guid>.Success();
	}
}
