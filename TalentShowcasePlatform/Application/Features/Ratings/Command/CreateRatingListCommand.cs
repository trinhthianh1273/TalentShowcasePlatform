using Application.Features.Ratings.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ratings.Command;

public class CreateRatingListCommand : IRequest<Result<List<RatingDto>>>
{
	public List<CreateRatingCommand> Ratings { get; set; }
}

public class CreateRatingListCommandHandler : IRequestHandler<CreateRatingListCommand, Result<List<RatingDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateRatingListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<RatingDto>>> Handle(CreateRatingListCommand request, CancellationToken cancellationToken)
	{
		var ratings = new List<Rating>();
		foreach(var item in request.Ratings)
		{
			var rating = new Rating
			{
				Id = Guid.NewGuid(),
				VideoId = item.VideoId,
				UserId = item.UserId,
				Stars = item.Stars
			};
			ratings.Add(rating);
		}

		await _unitOfWork.Repository<Rating>().AddRangeAsync(ratings);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var ratingDtos = _mapper.Map<List<RatingDto>>(ratings);
			return Result<List<RatingDto>>.Success(ratingDtos, "Rate successfully");
		}
		else
		{
			return Result<List<RatingDto>>.Failure("Rate failed.");
		}
	}
}

