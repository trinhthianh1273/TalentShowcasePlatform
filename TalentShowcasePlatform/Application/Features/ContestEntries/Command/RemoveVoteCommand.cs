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

public record RemoveVoteCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class RemoveVoteCommandHandler : IRequestHandler<RemoveVoteCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public RemoveVoteCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(RemoveVoteCommand request, CancellationToken cancellationToken)
	{
		var contestEntry = await _unitOfWork.Repository<ContestEntry>().GetByIdAsync(request.Id);

		if (contestEntry == null)
		{
			return Result<bool>.Failure("Không tìm thấy mục tham gia cuộc thi.");
		}

		if (contestEntry.Votes > 0)
		{
			contestEntry.Votes--;
			await _unitOfWork.Repository<ContestEntry>().UpdateAsync(contestEntry);
			var saveResult = await _unitOfWork.Save(cancellationToken);

			if (saveResult > 0)
			{
				return Result<bool>.Success(true);
			}
			else
			{
				return Result<bool>.Failure("Không thể gỡ phiếu bầu.");
			}
		}
		else
		{
			return Result<bool>.Failure("Số phiếu bầu đã là 0.");
		}
	}
}
