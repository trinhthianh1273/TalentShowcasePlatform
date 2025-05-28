using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupMembers.Command;

public record RemoveUserFromGroupCommand : IRequest<Result<bool>>
{
	public Guid GroupId { get; set; }
	public Guid UserId { get; set; }
}

public class RemoveUserFromGroupCommandHandler : IRequestHandler<RemoveUserFromGroupCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IActivityEventPublisher _activityEventPublisher;

	public RemoveUserFromGroupCommandHandler(IUnitOfWork unitOfWork, IActivityEventPublisher activityEventPublisher)
	{
		_unitOfWork = unitOfWork;
		_activityEventPublisher = activityEventPublisher;
	}

	public async Task<Result<bool>> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
	{
		var groupMember = await _unitOfWork.Repository<GroupMember>()
			.Entities
			.Where(gm => gm.GroupId == request.GroupId && gm.UserId == request.UserId)
			.FirstOrDefaultAsync();

		if (groupMember == null)
		{
			return Result<bool>.Failure("Người dùng không có trong nhóm này.");
		}

		await _unitOfWork.Repository<GroupMember>().DeleteAsync(groupMember);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var groupJoined = await _unitOfWork.Repository<Group>().GetByIdAsync(request.GroupId);
			// Tạo notification
			await _activityEventPublisher.PublishOutGroupAsync(groupJoined.CreatedBy, groupJoined.CreatedBy, groupJoined.Id);

			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa người dùng khỏi nhóm.");
		}
	}
}


