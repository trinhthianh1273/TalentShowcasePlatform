using Application.Features.Views.Dto;
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

namespace Application.Features.Views.Query;

public record GetAllViewsQuery : IRequest<Result<IEnumerable<ViewDto>>>
{
}

public class GetAllViewsQueryHandler : IRequestHandler<GetAllViewsQuery, Result<IEnumerable<ViewDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllViewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<ViewDto>>> Handle(GetAllViewsQuery request, CancellationToken cancellationToken)
	{
		var views = await _unitOfWork.Repository<View>()
			.GetAllAsync(include: q => q.Include(v => v.Video).Include(v => v.Viewer));

		return Result<IEnumerable<ViewDto>>.Success(_mapper.Map<IEnumerable<ViewDto>>(views));
	}
}


