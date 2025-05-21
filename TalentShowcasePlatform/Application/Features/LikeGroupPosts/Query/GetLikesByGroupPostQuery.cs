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

namespace Application.Features.LikeGroupPosts.Query;

public record GetLikesByGroupPostQuery(Guid GroupPostId) : IRequest<Result<IEnumerable<LikeGroupPostDto>>>;
public class GetLikesByGroupPostQueryHandler : IRequestHandler<GetLikesByGroupPostQuery, Result<IEnumerable<LikeGroupPostDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetLikesByGroupPostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<LikeGroupPostDto>>> Handle(GetLikesByGroupPostQuery request, CancellationToken cancellationToken)
	{
		var likes = await _unitOfWork.Repository<LikeGroupPost>().Entities
			.Where(x => x.GroupPostId == request.GroupPostId)
			.Include(i => i.User)
			.ToListAsync(cancellationToken);

		var dtos = _mapper.Map<IEnumerable<LikeGroupPostDto>>(likes);
		return Result<IEnumerable<LikeGroupPostDto>>.Success(dtos);
	}
}

