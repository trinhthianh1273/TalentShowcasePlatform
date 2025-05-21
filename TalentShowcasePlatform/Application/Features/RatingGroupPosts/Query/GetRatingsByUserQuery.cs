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

public record GetRatingsByUserQuery(Guid UserId) : IRequest<Result<IEnumerable<RatingGroupPostDto>>>;

public class GetRatingsByUserQueryHandler : IRequestHandler<GetRatingsByUserQuery, Result<IEnumerable<RatingGroupPostDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetRatingsByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<RatingGroupPostDto>>> Handle(GetRatingsByUserQuery request, CancellationToken cancellationToken)
	{
		var ratings = await _unitOfWork.Repository<RatingGroupPost>().Entities
			.Where(r => r.UserId == request.UserId)
			.ToListAsync(cancellationToken);

		if (!ratings.Any())
			return Result<IEnumerable<RatingGroupPostDto>>.Success(new List<RatingGroupPostDto>());

		var dtos = _mapper.Map<IEnumerable<RatingGroupPostDto>>(ratings);
		return Result<IEnumerable<RatingGroupPostDto>>.Success(dtos);
	}
}
