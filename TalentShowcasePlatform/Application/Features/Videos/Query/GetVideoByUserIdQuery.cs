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

public record GetVideoByUserIdQuery : IRequest<Result<IEnumerable<VideoDto>>>
{
	public Guid Id { get; set; }
}

public class GetVideoByUserIdQueryHandler : IRequestHandler<GetVideoByUserIdQuery, Result<IEnumerable<VideoDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetVideoByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<VideoDto>>> Handle(GetVideoByUserIdQuery request, CancellationToken cancellationToken)
	{
		var videos =  _unitOfWork.Repository<Video>().Entities
									.Where(v => v.UserId == request.Id)
									.Include(v => v.User)
									.Include(v => v.Category)
									.OrderByDescending(v => v.UploadedAt); ;

		if (videos == null)
		{
			return Result<IEnumerable<VideoDto>>.Failure("Video not found.");
		}

		return Result<IEnumerable<VideoDto>>.Success(_mapper.Map<IEnumerable<VideoDto>>(videos));
	}
}
