using Application.Features.ContestEntries.Dto;
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

namespace Application.Features.ContestEntries.Query;

public class GetAllContestEntriesQuery : IRequest<Result<IEnumerable<ContestEntryDto>>>
{
}

public class GetAllContestEntriesQueryHandler : IRequestHandler<GetAllContestEntriesQuery, Result<IEnumerable<ContestEntryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllContestEntriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<ContestEntryDto>>> Handle(GetAllContestEntriesQuery request, CancellationToken cancellationToken)
	{
		var contestEntries = await _unitOfWork.Repository<ContestEntry>()
			.GetAllAsync(include: q => q.Include(ce => ce.Contest).Include(ce => ce.Video));
		return Result<IEnumerable<ContestEntryDto>>.Success(_mapper.Map<IEnumerable<ContestEntryDto>>(contestEntries));
	}
}
