using Application.Features.VideoLikes.Dto;
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

namespace Application.Features.VideoLikes.Query;

public record GetVideoLikeByVideoIdQuery : IRequest<Result<IEnumerable<VideoLikeDto>>>
{
	public Guid VideoId { get; set; }
}

internal class GetVideoLikeByVideoIdQueryHandler : IRequestHandler<GetVideoLikeByVideoIdQuery, Result<IEnumerable<VideoLikeDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetVideoLikeByVideoIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<VideoLikeDto>>> Handle(GetVideoLikeByVideoIdQuery request, CancellationToken cancellationToken)
	{
		var videoLike = await _unitOfWork.Repository<VideoLike>().Entities
										.Where(i => i.VideoId == request.VideoId)
										.Include(i => i.Video)
										.Include(i => i.User)
										.ToListAsync(cancellationToken);

		if (videoLike == null)
		{
			return Result<IEnumerable<VideoLikeDto>>.Failure("Video like not found.");
		}

		return Result<IEnumerable<VideoLikeDto>>.Success(_mapper.Map<IEnumerable<VideoLikeDto>>(videoLike));
	}
}
