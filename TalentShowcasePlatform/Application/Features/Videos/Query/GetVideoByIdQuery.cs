using Application.Features.Videos.Dto;
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

namespace Application.Features.Videos.Query;

public class GetVideoByIdQuery : IRequest<Result<VideoDto>>
{
	public Guid Id { get; set; }
}

public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, Result<VideoDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetVideoByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<VideoDto>> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
	{
		var video = await _unitOfWork.Repository<Video>()
			.GetByIdAsync(request.Id, include: q => q
				.Include(v => v.User)
				.Include(v => v.Category)
				.Include(v => v.CommentVideos)
				.Include(v => v.VideoLikes)
				.Include(v => v.Views)
				// Include other navigation properties if needed, but be mindful of performance
				);

		if (video == null)
		{
			return Result<VideoDto>.Failure("Video not found.");
		}

		return Result<VideoDto>.Success(_mapper.Map<VideoDto>(video));
	}
}


