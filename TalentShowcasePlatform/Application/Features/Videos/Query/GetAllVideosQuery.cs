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

public class GetAllVideosQuery : IRequest<Result<IEnumerable<VideoDto>>>
{
}

public class GetAllVideosQueryHandler : IRequestHandler<GetAllVideosQuery, Result<IEnumerable<VideoDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllVideosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<VideoDto>>> Handle(GetAllVideosQuery request, CancellationToken cancellationToken)
	{
		var videos = await _unitOfWork.Repository<Video>()
			.GetAllAsync(include: q => q
				.Include(v => v.User)
				.Include(v => v.Category)
				// Include other navigation properties if needed, but be mindful of performance
				);

		return Result<IEnumerable<VideoDto>>.Success(_mapper.Map<IEnumerable<VideoDto>>(videos));
	}
}


