using Application.Features.LikeCommentGroupPosts.Dto;
using AutoMapper;
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

namespace Application.Features.LikeCommentGroupPosts.Command;

public record CreateLikeCommentGroupPostCommand(Guid UserId, Guid CommentGroupPostId) : IRequest<Result<LikeCommentGroupPostDto>>;
public class CreateLikeCommentGroupPostCommandHandler : IRequestHandler<CreateLikeCommentGroupPostCommand, Result<LikeCommentGroupPostDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IActivityEventPublisher _activityEventPublisher;

	public CreateLikeCommentGroupPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IActivityEventPublisher activityEventPublisher)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_activityEventPublisher = activityEventPublisher;
	}

	public async Task<Result<LikeCommentGroupPostDto>> Handle(CreateLikeCommentGroupPostCommand request, CancellationToken cancellationToken)
	{
		var exists = await _unitOfWork.Repository<LikeCommentGroupPost>().Entities
			.AnyAsync(x => x.UserId == request.UserId && x.CommentGroupPostId == request.CommentGroupPostId, cancellationToken);

		if (exists)
			return Result<LikeCommentGroupPostDto>.Failure("User already liked this comment.");

		var like = new LikeCommentGroupPost
		{
			Id = Guid.NewGuid(),
			UserId = request.UserId,
			CommentGroupPostId = request.CommentGroupPostId,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<LikeCommentGroupPost>().AddAsync(like);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var comment = await _unitOfWork.Repository<CommentGroupPost>().GetByIdAsync(request.CommentGroupPostId);
			// Tạo notification
			await _activityEventPublisher.PublishLikeCommentGroupPostAsync(like.UserId, comment.UserId, comment.GroupPostId, like.Id);

			var dto = _mapper.Map<LikeCommentGroupPostDto>(like);
			return Result<LikeCommentGroupPostDto>.Success(dto);
		}
		else
		{
			return Result<LikeCommentGroupPostDto>.Failure("Không thể like comment.");
		}
	}
}

