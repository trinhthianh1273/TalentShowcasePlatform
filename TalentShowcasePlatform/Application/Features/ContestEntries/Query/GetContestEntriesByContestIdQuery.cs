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

public record GetContestEntriesByContestIdQuery : IRequest<Result<IEnumerable<ContestEntryDto>>>
{
	public Guid ContestId { get; set; }
}

public class GetContestEntriesByContestIdQueryHandler : IRequestHandler<GetContestEntriesByContestIdQuery, Result<IEnumerable<ContestEntryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetContestEntriesByContestIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<ContestEntryDto>>> Handle(GetContestEntriesByContestIdQuery request, CancellationToken cancellationToken)
	{
		var contestEntries =  _unitOfWork.Repository<ContestEntry>().Entities
			.Where(ce => ce.ContestId == request.ContestId)
			.Include(ce => ce.Contest)
			.Include(ce => ce.Video);
		return Result<IEnumerable<ContestEntryDto>>.Success(_mapper.Map<IEnumerable<ContestEntryDto>>(contestEntries));
	}
}

