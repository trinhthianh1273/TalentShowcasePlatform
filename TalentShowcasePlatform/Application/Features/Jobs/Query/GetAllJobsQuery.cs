using Application.Features.Jobs.Dto;
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

namespace Application.Features.Jobs.Query;

public record GetAllJobsQuery : IRequest<Result<IEnumerable<JobShortDto>>>
{
}

public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, Result<IEnumerable<JobShortDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetAllJobsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<JobShortDto>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
	{
		var jobs = await _unitOfWork.Repository<Job>()
			.GetAllAsync(include: q => q.Include(j => j.PostedByUser).Include(j => j.Category));

		return Result<IEnumerable<JobShortDto>>.Success(_mapper.Map<IEnumerable<JobShortDto>>(jobs));
	}
}


