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

namespace Application.Features.LikeCommentGroupPosts.Query;

public record GetCommentLikesByUserQuery(Guid UserId) : IRequest<Result<IEnumerable<LikeCommentGroupPostDto>>>;
public class GetCommentLikesByUserQueryHandler : IRequestHandler<GetCommentLikesByUserQuery, Result<IEnumerable<LikeCommentGroupPostDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetCommentLikesByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<LikeCommentGroupPostDto>>> Handle(GetCommentLikesByUserQuery request, CancellationToken cancellationToken)
	{
		var likes = await _unitOfWork.Repository<LikeCommentGroupPost>().Entities
			.Where(x => x.UserId == request.UserId)
			.ToListAsync(cancellationToken);

		var dtos = _mapper.Map<IEnumerable<LikeCommentGroupPostDto>>(likes);
		return Result<IEnumerable<LikeCommentGroupPostDto>>.Success(dtos);
	}
}

