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

public record DeleteContestEntryCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteContestEntryCommandHandler : IRequestHandler<DeleteContestEntryCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteContestEntryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteContestEntryCommand request, CancellationToken cancellationToken)
	{
		var contestEntry = await _unitOfWork.Repository<ContestEntry>().GetByIdAsync(request.Id);

		if (contestEntry == null)
		{
			return Result<bool>.Failure("Không tìm thấy mục tham gia cuộc thi.");
		}

		await _unitOfWork.Repository<ContestEntry>().DeleteAsync(contestEntry);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa mục tham gia cuộc thi.");
		}
	}
}
