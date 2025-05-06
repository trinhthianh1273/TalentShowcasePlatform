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

public class GetVideoLikeByIdQuery : IRequest<Result<VideoLikeDto>>
{
	public Guid Id { get; set; }
}

public class GetVideoLikeByIdQueryHandler : IRequestHandler<GetVideoLikeByIdQuery, Result<VideoLikeDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetVideoLikeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<VideoLikeDto>> Handle(GetVideoLikeByIdQuery request, CancellationToken cancellationToken)
	{
		var videoLike = await _unitOfWork.Repository<VideoLike>()
			.GetByIdAsync(request.Id, include: q => q.Include(vl => vl.Video).Include(vl => vl.User));

		if (videoLike == null)
		{
			return Result<VideoLikeDto>.Failure("Video like not found.");
		}

		return Result<VideoLikeDto>.Success(_mapper.Map<VideoLikeDto>(videoLike));
	}
}



