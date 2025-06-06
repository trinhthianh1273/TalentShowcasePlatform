﻿using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Jobs.Command;

public record CreateJobCommand : IRequest<Result<Guid>>
{
	public string Title { get; set; }
	public string? CompanyName { get; set; }
	public string Location { get; set; }
	public string AddressDetail { get; set; }
	public string Description { get; set; }
	public string Requirements { get; set; }
	public string Benefits { get; set; }
	public string JobType { get; set; }
	public decimal SalaryFrom { get; set; }
	public decimal SalaryTo { get; set; }
	public DateTime? ExpiryDate { get; set; }
	public string ContactEmail { get; set; }
	public string ContactPhone { get; set; }
	public Guid PostedBy { get; set; }
	public Guid CategoryId { get; set; }
}

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateJobCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
	{
		var job = new Job
		{
			Id = Guid.NewGuid(),
			Title = request.Title,
			CompanyName = request.CompanyName,
			Location = request.Location,
			AddressDetail = request.AddressDetail,
			Description = request.Description,
			Requirements = request.Requirements,
			Benefits = request.Benefits,
			JobType = request.JobType,
			SalaryFrom = request.SalaryFrom,
			SalaryTo = request.SalaryTo,
			ExpiryDate = request.ExpiryDate,
			ContactEmail = request.ContactEmail,
			ContactPhone = request.ContactPhone,
			PostedBy = request.PostedBy,
			CategoryId = request.CategoryId
		};

		await _unitOfWork.Repository<Job>().AddAsync(job);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(job.Id);
		}
		else
		{
			return Result<Guid>.Failure("Failed to create job.");
		}
	}
}

