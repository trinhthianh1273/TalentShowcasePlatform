﻿using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GroupPostComments.Command;

public record CreateGroupPostCommentCommand : IRequest<Result<Guid>>
{
	public Guid GroupPostId { get; set; }
	public Guid UserId { get; set; }
	public string Content { get; set; }
	public Guid? ParentCommentId { get; set; }
}

public class CreateGroupPostCommentCommandHandler : IRequestHandler<CreateGroupPostCommentCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IActivityEventPublisher _activityEventPublisher;

	public CreateGroupPostCommentCommandHandler(IUnitOfWork unitOfWork, IActivityEventPublisher activityEventPublisher)
	{
		_unitOfWork = unitOfWork;
		_activityEventPublisher = activityEventPublisher;
	}

	public async Task<Result<Guid>> Handle(CreateGroupPostCommentCommand request, CancellationToken cancellationToken)
	{
		var newGroupPostComment = new CommentGroupPost
		{
			Id = Guid.NewGuid(),
			GroupPostId = request.GroupPostId,
			UserId = request.UserId,
			Content = request.Content,
			ParentCommentId = request.ParentCommentId,
		};

		await _unitOfWork.Repository<CommentGroupPost>().AddAsync(newGroupPostComment);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var groupPost = await _unitOfWork.Repository<GroupPost>().GetByIdAsync(request.GroupPostId);
			// Tạo notification
			await _activityEventPublisher.PublishCommentGroupPostAsync(newGroupPostComment.UserId, groupPost.UserId, newGroupPostComment.GroupPostId, newGroupPostComment.Id);

			return Result<Guid>.Success(newGroupPostComment.Id);
		}
		else
		{
			return Result<Guid>.Failure("Error to comment.");
		}
	}
}
