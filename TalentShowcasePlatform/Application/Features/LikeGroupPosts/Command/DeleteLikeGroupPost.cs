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

public record DeleteLikeGroupPostCommand(Guid UserId, Guid GroupPostId) : IRequest<Result<Guid>>;

public class DeleteLikeGroupPostCommandHandler : IRequestHandler<DeleteLikeGroupPostCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteLikeGroupPostCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(DeleteLikeGroupPostCommand request, CancellationToken cancellationToken)
	{
		var like = await _unitOfWork.Repository<LikeGroupPost>().Entities
			.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.GroupPostId == request.GroupPostId, cancellationToken);

		if (like is null)
			return Result<Guid>.Failure("Like not found.");

		await _unitOfWork.Repository<LikeGroupPost>().DeleteAsync(like);
		await _unitOfWork.Save(cancellationToken);

		return Result<Guid>.Success(like.Id);
	}
}

