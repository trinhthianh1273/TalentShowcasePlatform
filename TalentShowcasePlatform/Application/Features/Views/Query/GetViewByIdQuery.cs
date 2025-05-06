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

public class GetViewByIdQuery : IRequest<Result<ViewDto>>
{
	public Guid Id { get; set; }
}

public class GetViewByIdQueryHandler : IRequestHandler<GetViewByIdQuery, Result<ViewDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetViewByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<ViewDto>> Handle(GetViewByIdQuery request, CancellationToken cancellationToken)
	{
		var view = await _unitOfWork.Repository<View>()
			.GetByIdAsync(request.Id, include: q => q.Include(v => v.Video).Include(v => v.Viewer));

		if (view == null)
		{
			return Result<ViewDto>.Failure("View not found.");
		}

		return Result<ViewDto>.Success(_mapper.Map<ViewDto>(view));
	}
}



