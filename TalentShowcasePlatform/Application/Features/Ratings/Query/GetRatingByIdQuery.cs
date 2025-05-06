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

public class GetRatingByIdQuery : IRequest<Result<RatingDto>>
{
	public Guid Id { get; set; }
}

public class GetRatingByIdQueryHandler : IRequestHandler<GetRatingByIdQuery, Result<RatingDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetRatingByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<RatingDto>> Handle(GetRatingByIdQuery request, CancellationToken cancellationToken)
	{
		var rating = await _unitOfWork.Repository<Rating>()
			.GetByIdAsync(request.Id, include: q => q.Include(r => r.Video).Include(r => r.User));

		if (rating == null)
		{
			return Result<RatingDto>.Failure("Rating not found.");
		}

		return Result<RatingDto>.Success(_mapper.Map<RatingDto>(rating));
	}
}



