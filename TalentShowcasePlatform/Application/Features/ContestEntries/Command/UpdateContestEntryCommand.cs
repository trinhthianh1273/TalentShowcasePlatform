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

public record UpdateContestEntryCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public int Votes { get; set; }
}

public class UpdateContestEntryCommandHandler : IRequestHandler<UpdateContestEntryCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateContestEntryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateContestEntryCommand request, CancellationToken cancellationToken)
	{
		var contestEntry = await _unitOfWork.Repository<ContestEntry>().GetByIdAsync(request.Id);

		if (contestEntry == null)
		{
			return Result<bool>.Failure("Không tìm thấy mục tham gia cuộc thi.");
		}

		contestEntry.Votes = request.Votes;

		await _unitOfWork.Repository<ContestEntry>().UpdateAsync(contestEntry);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể cập nhật số phiếu bầu.");
		}
	}
}
