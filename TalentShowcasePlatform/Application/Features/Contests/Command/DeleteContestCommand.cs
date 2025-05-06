using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Contests.Command;

public record DeleteContestCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteContestCommandHandler : IRequestHandler<DeleteContestCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteContestCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteContestCommand request, CancellationToken cancellationToken)
	{
		var contest = await _unitOfWork.Repository<Contest>().GetByIdAsync(request.Id);

		if (contest == null)
		{
			return Result<bool>.Failure("Không tìm thấy cuộc thi.");
		}

		await _unitOfWork.Repository<Contest>().DeleteAsync(contest);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa cuộc thi.");
		}
	}
}

