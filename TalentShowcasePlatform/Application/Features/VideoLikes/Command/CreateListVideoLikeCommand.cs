using Application.Features.VideoLikes.Dto;
using AutoMapper;
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

public class CreateListVideoLikeCommand : IRequest<Result<List<VideoLikeDto>>>
{
	public List<CreateVideoLikeCommand> Likes { get; set; }
}

public class CreateListVideoLikeCommandHandler : IRequestHandler<CreateListVideoLikeCommand, Result<List<VideoLikeDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IActivityEventPublisher _activityEventPublisher;

	public CreateListVideoLikeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IActivityEventPublisher activityEventPublisher	)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_activityEventPublisher = activityEventPublisher;
	}

	public async Task<Result<List<VideoLikeDto>>> Handle(CreateListVideoLikeCommand request, CancellationToken cancellationToken)
	{
		var likes = new List<VideoLike>();
		foreach (var item in request.Likes)
		{
			var videoLike = new VideoLike
			{
				Id = Guid.NewGuid(),
				VideoId = item.VideoId,
				UserId = item.UserId
			};
			likes.Add(videoLike);
		} 

		await _unitOfWork.Repository<VideoLike>().AddRangeAsync(likes);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			
			var videoLikeDtos = _mapper.Map<List<VideoLikeDto>>(likes);
			return Result<List<VideoLikeDto>>.Success(videoLikeDtos, "Like Successfully");
		}
		else
		{
			return Result<List<VideoLikeDto>>.Failure("Like Video faile.");
		}
	}
}




