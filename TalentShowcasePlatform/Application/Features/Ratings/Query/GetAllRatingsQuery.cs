using Application.Features.Ratings.Dto;
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

namespace Application.Features.Ratings.Query;

public class GetAllRatingsQuery : IRequest<Result<IEnumerable<RatingDto>>>
{
}

public class GetAllRatingsQueryHandler : IRequestHandler<GetAllRatingsQuery, Result<IEnumerable<RatingDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllRatingsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<RatingDto>>> Handle(GetAllRatingsQuery request, CancellationToken cancellationToken)
	{
		var ratings = await _unitOfWork.Repository<Rating>()
			.GetAllAsync(include: q => q.Include(r => r.Video).Include(r => r.User));

		return Result<IEnumerable<RatingDto>>.Success(_mapper.Map<IEnumerable<RatingDto>>(ratings));
	}
}




