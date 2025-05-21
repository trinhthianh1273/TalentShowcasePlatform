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

public record DeleteLikeCommentGroupPostCommand(Guid UserId, Guid CommentGroupPostId) : IRequest<Result<Guid>>;
public class DeleteLikeCommentGroupPostCommandHandler : IRequestHandler<DeleteLikeCommentGroupPostCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteLikeCommentGroupPostCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(DeleteLikeCommentGroupPostCommand request, CancellationToken cancellationToken)
	{
		var like = await _unitOfWork.Repository<LikeCommentGroupPost>().Entities
			.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.CommentGroupPostId == request.CommentGroupPostId, cancellationToken);

		if (like == null)
			return Result<Guid>.Failure("Like not found.");

		await _unitOfWork.Repository<LikeCommentGroupPost>().DeleteAsync(like);
		await _unitOfWork.Save(cancellationToken);

		return Result<Guid>.Success(like.Id);
	}
}

