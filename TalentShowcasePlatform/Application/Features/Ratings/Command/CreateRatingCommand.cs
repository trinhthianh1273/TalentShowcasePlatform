using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ratings.Command;

public class CreateRatingCommand : IRequest<Result<Guid>>
{
	public Guid VideoId { get; set; }
	public Guid UserId { get; set; }
	public int Stars { get; set; }
}

public class CreateRatingHandler : IRequestHandler<CreateRatingCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateRatingHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
	{
		var rating = new Rating
		{
			Id = Guid.NewGuid(),
			VideoId = request.VideoId,
			UserId = request.UserId,
			Stars = request.Stars
		};

		await _unitOfWork.Repository<Rating>().AddAsync(rating);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(rating.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create rating.");
		}
	}
}

