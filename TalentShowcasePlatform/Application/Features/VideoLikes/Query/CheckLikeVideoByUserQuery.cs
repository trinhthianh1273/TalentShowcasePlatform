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

public record CheckLikeVideoByUserQuery : IRequest<Result<Guid>>
{
	public Guid UserId { get; set; }
	public Guid VideoId { get; set; }

	public CheckLikeVideoByUserQuery(Guid userId, Guid videoId)
	{
		UserId = userId;
		VideoId = videoId;
	}
}

internal class CheckLikeVideoByUserQueryHandler : IRequestHandler<CheckLikeVideoByUserQuery, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CheckLikeVideoByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<Guid>> Handle(CheckLikeVideoByUserQuery request, CancellationToken cancellationToken)
	{
		// This method should check if a user has liked a video and return the VideoLike ID if it exists.
		var result = await _unitOfWork.Repository<VideoLike>()
						.Entities
						.Where(vl => vl.UserId == request.UserId && vl.VideoId == request.VideoId)
						.Select(vl => vl.Id)
						.FirstOrDefaultAsync(cancellationToken);
		if (result == Guid.Empty)
		{
			// If no like exists, return a failure result with a message.
			return Result<Guid>.Failure("No like found for this video by the user.");
		}
		else
		{
			// If a like exists, return the VideoLike ID.
			return Result<Guid>.Success(result);
		} 
	}
}
