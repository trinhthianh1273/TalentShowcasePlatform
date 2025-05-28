using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupMembers.Command;

public record AddUserToGroupCommand : IRequest<Result<Guid>>
{
	public Guid GroupId { get; set; }
	public Guid UserId { get; set; }
}

public class AddUserToGroupCommandHandler : IRequestHandler<AddUserToGroupCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IActivityEventPublisher _activityEventPublisher;

	public AddUserToGroupCommandHandler(IUnitOfWork unitOfWork, IActivityEventPublisher activityEventPublisher)
	{
		_unitOfWork = unitOfWork;
		_activityEventPublisher = activityEventPublisher;
	}

	public async Task<Result<Guid>> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
	{
		// Check if the user is already in the group
		var existingMember = await _unitOfWork.Repository<GroupMember>()
			.FindAsync(gm => gm.GroupId == request.GroupId && gm.UserId == request.UserId);

		if (existingMember.Any())
		{
			return Result<Guid>.Failure("Người dùng đã ở trong nhóm này.");
		}

		var groupMember = new GroupMember
		{
			Id = Guid.NewGuid(),
			GroupId = request.GroupId,
			UserId = request.UserId,
			JoinedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<GroupMember>().AddAsync(groupMember);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var groupJoined = await _unitOfWork.Repository<Group>().GetByIdAsync(request.GroupId);
			// Tạo notification
			await _activityEventPublisher.PublishJoinGroupAsync(groupMember.UserId, groupJoined.CreatedBy, groupJoined.Id);

			return Result<Guid>.Success(groupMember.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không thể thêm người dùng vào nhóm.");
		}
	}
}
