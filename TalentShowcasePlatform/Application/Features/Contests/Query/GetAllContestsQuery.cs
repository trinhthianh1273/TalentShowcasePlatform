using Application.Features.Contests.Dto;
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

namespace Application.Features.Contests.Query;

public class GetAllContestsQuery : IRequest<Result<IEnumerable<ContestDto>>>
{
}

public class GetAllContestsQueryHandler : IRequestHandler<GetAllContestsQuery, Result<IEnumerable<ContestDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllContestsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<ContestDto>>> Handle(GetAllContestsQuery request, CancellationToken cancellationToken)
	{
		var contests = await _unitOfWork.Repository<Contest>()
			.GetAllAsync(include: q => q.Include(c => c.CreatedByUser));
		return Result<IEnumerable<ContestDto>>.Success(_mapper.Map<IEnumerable<ContestDto>>(contests));
	}
}
