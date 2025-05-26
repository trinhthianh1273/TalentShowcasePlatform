using Application.Features.Jobs.Dto;
using Application.Features.Jobs.Query;
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

public record GetJobByUserIdQuery : IRequest<Result<IEnumerable<JobDto>>>
{
	public Guid UserId { get; set; }
}

internal class GetJobByUserIdQueryHandler : IRequestHandler<GetJobByUserIdQuery, Result<IEnumerable<JobDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetJobByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<JobDto>>> Handle(GetJobByUserIdQuery request, CancellationToken cancellationToken)
	{
		var jobs = await _unitOfWork.Repository<Job>().Entities
								.Where(i => i.PostedBy == request.UserId)
								.Include(i => i.PostedByUser)
								.Include(i => i.Category)
								.ToListAsync(cancellationToken);

		if (jobs == null)
		{
			return Result<IEnumerable<JobDto>>.Failure("Job not found.");
		}

		return Result<IEnumerable<JobDto>>.Success(_mapper.Map<IEnumerable<JobDto>>(jobs));
	}
}
