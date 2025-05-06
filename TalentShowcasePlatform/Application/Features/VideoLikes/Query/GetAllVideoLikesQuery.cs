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

public class GetAllVideoLikesQuery : IRequest<Result<IEnumerable<VideoLikeDto>>>
{
}

public class GetAllVideoLikesQueryHandler : IRequestHandler<GetAllVideoLikesQuery, Result<IEnumerable<VideoLikeDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllVideoLikesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<VideoLikeDto>>> Handle(GetAllVideoLikesQuery request, CancellationToken cancellationToken)
	{
		var videoLikes = await _unitOfWork.Repository<VideoLike>()
			.GetAllAsync(include: q => q.Include(vl => vl.Video).Include(vl => vl.User));

		return Result<IEnumerable<VideoLikeDto>>.Success(_mapper.Map<IEnumerable<VideoLikeDto>>(videoLikes));
	}
}


