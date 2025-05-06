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

public record GetJobByIdQuery : IRequest<Result<JobDto>>
{
	public Guid Id { get; set; }
}

public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Result<JobDto>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public GetJobByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<JobDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
	{
		var job = await _unitOfWork.Repository<Job>()
			.GetByIdAsync(request.Id, include: q => q.Include(j => j.PostedByUser).Include(j => j.Category));

		if (job == null)
		{
			return Result<JobDto>.Failure("Job not found.");
		}

		return Result<JobDto>.Success(_mapper.Map<JobDto>(job));
	}
}



