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

public record DeleteJobCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteJobCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
	{
		var job = await _unitOfWork.Repository<Job>().GetByIdAsync(request.Id);

		if (job == null)
		{
			return Result<bool>.Failure("Job not found.");
		}

		await _unitOfWork.Repository<Job>().DeleteAsync(job);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Failed to delete job.");
		}
	}
}


