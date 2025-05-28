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

public class CreateNotificationCommand : IRequest<Result<Guid>>
{
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string Message { get; set; }
	public string Type { get; set; }
	public Guid? RelatedEntityId { get; set; }
	public RelatedEntityType RelatedEntityType { get; set; }
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
			Title = request.Title,
			Message = request.Message,
			Type = request.Type,
			RelatedEntityId = request.RelatedEntityId,
			RelatedEntityType = request.RelatedEntityType
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