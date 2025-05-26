using Application.Features.LikeGroupPosts.Dto;
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

namespace Application.Features.LikeGroupPosts.Query;

public record GetLikePostIdUserIdQuery(Guid PostId, Guid UserId) : IRequest<Result<Guid>>;

internal class GetLikePostIdUserIdQueryHandler : IRequestHandler<GetLikePostIdUserIdQuery, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetLikePostIdUserIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(GetLikePostIdUserIdQuery request, CancellationToken cancellationToken)
	{
		var isLike = await _unitOfWork.Repository<LikeGroupPost>().Entities
							.Where(i => i.GroupPostId == request.PostId && i.UserId == request.UserId)
							.FirstOrDefaultAsync();
		return Result<Guid>.Success(isLike.Id);
	}
}
