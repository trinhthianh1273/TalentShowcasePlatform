using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VideoLikes.Command;

public class DeleteVideoLikeCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteVideoLikeHandler : IRequestHandler<DeleteVideoLikeCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteVideoLikeHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteVideoLikeCommand request, CancellationToken cancellationToken)
	{
		var videoLike = await _unitOfWork.Repository<VideoLike>().GetByIdAsync(request.Id);

		if (videoLike == null)
		{
			return Result<bool>.Failure("Video like not found.");
		}

		await _unitOfWork.Repository<VideoLike>().DeleteAsync(videoLike);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete video like.");
		}
	}
}


