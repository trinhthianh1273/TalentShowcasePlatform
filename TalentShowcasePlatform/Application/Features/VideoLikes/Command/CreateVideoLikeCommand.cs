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

public class CreateVideoLikeCommand : IRequest<Result<Guid>>
{
	public Guid VideoId { get; set; }
	public Guid UserId { get; set; }
	public DateTime LikedAt { get; set; }
}

public class CreateVideoLikeHandler : IRequestHandler<CreateVideoLikeCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateVideoLikeHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateVideoLikeCommand request, CancellationToken cancellationToken)
	{
		var videoLike = new VideoLike
		{
			Id = Guid.NewGuid(),
			VideoId = request.VideoId,
			UserId = request.UserId,
			LikedAt = request.LikedAt
		};

		await _unitOfWork.Repository<VideoLike>().AddAsync(videoLike);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(videoLike.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create video like.");
		}
	}
}




