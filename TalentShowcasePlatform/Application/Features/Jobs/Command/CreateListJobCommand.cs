using Application.Features.Jobs.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Jobs.Command;

public record CreateListJobCommand : IRequest<Result<List<JobDto>>>
{
	public List<CreateJobCommand> Jobs { get; set; }
}

public class CreateListJobCommandHandler : IRequestHandler<CreateListJobCommand, Result<List<JobDto>>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateListJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<List<JobDto>>> Handle(CreateListJobCommand request, CancellationToken cancellationToken)
	{
		var jobs = new List<Job>();
		foreach (var item in request.Jobs)
		{
			var job = new Job
			{
				Id = Guid.NewGuid(),
				Title = item.Title,
				Description = item.Description,
				PostedBy = item.PostedBy,
				CategoryId = item.CategoryId,
				Location = item.Location,
				JobType = item.JobType,
				SalaryFrom = item.SalaryFrom,
				SalaryTo = item.SalaryTo,
				ExpiryDate = item.ExpiryDate,
				ContactEmail = item.ContactEmail,
				CreatedAt = DateTime.UtcNow
			};
			jobs.Add(job);
		}

		await _unitOfWork.Repository<Job>().AddRangeAsync(jobs);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			var jobDtos = _mapper.Map<List<JobDto>>(jobs);
			return Result<List<JobDto>>.Success(jobDtos, "Created job successfully.");
		}
		else
		{
			return Result<List<JobDto>>.Failure("Failed to create job.");
		}
	}
}

