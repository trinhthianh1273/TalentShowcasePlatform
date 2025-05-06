using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContestEntries.Command;

public record CreateContestEntryCommand : IRequest<Result<Guid>>
{
	public Guid ContestId { get; set; }
	public Guid VideoId { get; set; }
}

public class CreateContestEntryCommandHandler : IRequestHandler<CreateContestEntryCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateContestEntryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateContestEntryCommand request, CancellationToken cancellationToken)
	{
		// Check if the entry already exists (optional, based on your business logic)
		var existingEntry = await _unitOfWork.Repository<ContestEntry>()
			.FindAsync(e => e.ContestId == request.ContestId && e.VideoId == request.VideoId);

		if (existingEntry.Any())
		{
			return Result<Guid>.Failure("Video này đã được thêm vào cuộc thi.");
		}

		var contestEntry = new ContestEntry
		{
			Id = Guid.NewGuid(),
			ContestId = request.ContestId,
			VideoId = request.VideoId,
			SubmittedAt = DateTime.UtcNow,
			Votes = 0
		};

		await _unitOfWork.Repository<ContestEntry>().AddAsync(contestEntry);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(contestEntry.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không thể thêm video vào cuộc thi.");
		}
	}
}
