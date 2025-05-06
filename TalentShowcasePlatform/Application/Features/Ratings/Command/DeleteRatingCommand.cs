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

public class DeleteRatingCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteRatingHandler : IRequestHandler<DeleteRatingCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteRatingHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
	{
		var rating = await _unitOfWork.Repository<Rating>().GetByIdAsync(request.Id);

		if (rating == null)
		{
			return Result<bool>.Failure("Rating not found.");
		}

		await _unitOfWork.Repository<Rating>().DeleteAsync(rating);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete rating.");
		}
	}
}



