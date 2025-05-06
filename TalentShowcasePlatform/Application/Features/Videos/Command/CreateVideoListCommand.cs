using Application.Features.Videos.Dto;
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

namespace Application.Features.Videos.Command;

public class CreateVideoListCommand : IRequest<Result<List<VideoDto>>>
{
	public List<CreateVideoCommand> Videos { get; set; }
}

public class CreateVideosCommandHandler : IRequestHandler<CreateVideoListCommand, Result<List<VideoDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateVideosCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<VideoDto>>> Handle(CreateVideoListCommand request, CancellationToken cancellationToken)
	{
		var videos = new List<Video>();
		foreach (var videoDto in request.Videos)
		{
			var video = new Video
			{
				Id = Guid.NewGuid(),
				Title = videoDto.Title,
				Description = videoDto.Description,
				Url = videoDto.Url,
				UserId = videoDto.UserId,
				CategoryId = videoDto.CategoryId,
				IsPrivate = videoDto.IsPrivate,
				UploadedAt = DateTime.UtcNow
			};
			videos.Add(video);
		}

		await _unitOfWork.Repository<Video>().AddRangeAsync(videos); // Thêm nhiều video
		var result = await _unitOfWork.Save(cancellationToken);

		if (result > 0)
		{
			var createdVideoDtos = _mapper.Map<List<VideoDto>>(videos);
			return Result<List<VideoDto>>.Success(createdVideoDtos, "Videos created successfully.");
		}
		else
		{
			return Result<List<VideoDto>>.Failure("Failed to create videos.");
		}
	}
}



