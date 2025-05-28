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
}

public class CreateVideoLikeHandler : IRequestHandler<CreateVideoLikeCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IActivityEventPublisher _activityEventPublisher;

	public CreateVideoLikeHandler(IUnitOfWork unitOfWork, IActivityEventPublisher activityEventPublisher)
	{
		_unitOfWork = unitOfWork;
		_activityEventPublisher = activityEventPublisher;
	}

	public async Task<Result<Guid>> Handle(CreateVideoLikeCommand request, CancellationToken cancellationToken)
	{
		var videoLike = new VideoLike
		{
			Id = Guid.NewGuid(),
			VideoId = request.VideoId,
			UserId = request.UserId
		};

		await _unitOfWork.Repository<VideoLike>().AddAsync(videoLike);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var video = await _unitOfWork.Repository<Video>().GetByIdAsync(request.VideoId);
			// Tạo notification
			await _activityEventPublisher.PublishLikeVideoAsync(request.UserId, video.UserId, request.VideoId);

			return Result<Guid>.Success(videoLike.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create video like.");
		}
	}
}




