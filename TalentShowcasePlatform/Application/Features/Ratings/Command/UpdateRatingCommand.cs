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

public class UpdateRatingCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public Guid VideoId { get; set; }
	public Guid UserId { get; set; }
	public int Stars { get; set; }
}

public class UpdateRatingHandler : IRequestHandler<UpdateRatingCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateRatingHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
	{
		var rating = await _unitOfWork.Repository<Rating>().GetByIdAsync(request.Id);

		if (rating == null)
		{
			return Result<bool>.Failure("Rating not found.");
		}

		rating.VideoId = request.VideoId;
		rating.UserId = request.UserId;
		rating.Stars = request.Stars;

		await _unitOfWork.Repository<Rating>().UpdateAsync(rating);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to update rating.");
		}
	}
}



