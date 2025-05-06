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

public record UpdateJobCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public Guid CategoryId { get; set; }
	public string Location { get; set; }
	public string JobType { get; set; }
	public decimal? SalaryFrom { get; set; }
	public decimal? SalaryTo { get; set; }
	public DateTime? ExpiryDate { get; set; }
	public string ContactEmail { get; set; }
}

public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateJobCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
	{
		var job = await _unitOfWork.Repository<Job>().GetByIdAsync(request.Id);

		if (job == null)
		{
			return Result<bool>.Failure("Job not found.");
		}

		job.Title = request.Title;
		job.Description = request.Description;
		job.CategoryId = request.CategoryId;
		job.Location = request.Location;
		job.JobType = request.JobType;
		job.SalaryFrom = request.SalaryFrom;
		job.SalaryTo = request.SalaryTo;
		job.ExpiryDate = request.ExpiryDate;
		job.ContactEmail = request.ContactEmail;

		await _unitOfWork.Repository<Job>().UpdateAsync(job);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to update job.");
		}
	}
}



