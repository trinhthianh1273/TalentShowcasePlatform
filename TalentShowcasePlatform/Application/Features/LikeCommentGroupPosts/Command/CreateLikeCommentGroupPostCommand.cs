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

	public CreateLikeCommentGroupPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
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
		await _unitOfWork.Save(cancellationToken);

		var dto = _mapper.Map<LikeCommentGroupPostDto>(like);
		return Result<LikeCommentGroupPostDto>.Success(dto);
	}
}

