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

public record UpdateContestCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
}

public class UpdateContestCommandHandler : IRequestHandler<UpdateContestCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateContestCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateContestCommand request, CancellationToken cancellationToken)
	{
		var contest = await _unitOfWork.Repository<Contest>().GetByIdAsync(request.Id);

		if (contest == null)
		{
			return Result<bool>.Failure("Không tìm thấy cuộc thi.");
		}

		contest.Title = request.Title;
		contest.Description = request.Description;
		contest.StartDate = request.StartDate;
		contest.EndDate = request.EndDate;

		await _unitOfWork.Repository<Contest>().UpdateAsync(contest);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể cập nhật thông tin cuộc thi.");
		}
	}
}


