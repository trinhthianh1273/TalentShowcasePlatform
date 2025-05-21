using Application.Features.RatingGroupPosts.Dto;
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

namespace Application.Features.RatingGroupPosts.Query;

public record GetRatingsByGroupPostQuery(Guid GroupPostId) : IRequest<Result<IEnumerable<RatingGroupPostDto>>>;

public class GetRatingsByGroupPostQueryHandler : IRequestHandler<GetRatingsByGroupPostQuery, Result<IEnumerable<RatingGroupPostDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetRatingsByGroupPostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<RatingGroupPostDto>>> Handle(GetRatingsByGroupPostQuery request, CancellationToken cancellationToken)
	{
		var entities = await _unitOfWork.Repository<RatingGroupPost>().Entities
			.Where(r => r.GroupPostId == request.GroupPostId)
			.Include(i => i.User)
			.ToListAsync(cancellationToken);

		var dtos = _mapper.Map<IEnumerable<RatingGroupPostDto>>(entities);
		return Result<IEnumerable<RatingGroupPostDto>>.Success(dtos);
	}
}

