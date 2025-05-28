using Application.Features.LikeCommentGroupPosts.Dto;
using Application.Features.LikeGroupPosts.Dto;
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

namespace Application.Features.LikeGroupPosts.Command;

public record CreateLikeGroupPostCommand(Guid UserId, Guid GroupPostId) : IRequest<Result<Guid>>;

public class CreateLikeGroupPostCommandHandler : IRequestHandler<CreateLikeGroupPostCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IActivityEventPublisher _activityEventPublisher;

	public CreateLikeGroupPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IActivityEventPublisher activityEventPublisher)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_activityEventPublisher = activityEventPublisher;
	}

	public async Task<Result<Guid>> Handle(CreateLikeGroupPostCommand request, CancellationToken cancellationToken)
	{
		var exists = await _unitOfWork.Repository<LikeGroupPost>().Entities
			.AnyAsync(x => x.UserId == request.UserId && x.GroupPostId == request.GroupPostId, cancellationToken);

		if (exists)
			return Result<Guid>.Failure("User already liked this post.");

		var like = new LikeGroupPost
		{
			Id = Guid.NewGuid(),
			UserId = request.UserId,
			GroupPostId = request.GroupPostId,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<LikeGroupPost>().AddAsync(like);
		var saveResult = await _unitOfWork.Save(cancellationToken);
		if (saveResult > 0)
		{
			var post = await _unitOfWork.Repository<GroupPost>().GetByIdAsync(request.GroupPostId);
			// Tạo notification
			await _activityEventPublisher.PublishLikeGroupPostAsync(request.UserId, post.UserId, request.GroupPostId);

			var dto = _mapper.Map<LikeGroupPostDto>(like);
			return Result<Guid>.Success(dto.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không thể like post.");
		}
		
	}
}
