using Application.Features.Videos.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.Videos.Query;

public record GetVideoPaginationQuery : IRequest<PaginatedResult<VideoDto>>
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public GetVideoPaginationQuery()
	{
	}

	public GetVideoPaginationQuery(int pageNumber, int pageSize)
	{
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}

internal class GetVideoPaginationQueryHandle : IRequestHandler<GetVideoPaginationQuery, PaginatedResult<VideoDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetVideoPaginationQueryHandle(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<PaginatedResult<VideoDto>> Handle(GetVideoPaginationQuery request, CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Repository<Video>().Entities
								.Include(v => v.User)
								.Include(v => v.Category);
		var totalCount = await query.CountAsync(cancellationToken);

		var videos = await query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToListAsync(cancellationToken);
		var result = _mapper.Map<List<VideoDto>>(videos);
		//return result;
		return PaginatedResult<VideoDto>.Create(result, totalCount, request.PageNumber, request.PageSize);
	}
}
