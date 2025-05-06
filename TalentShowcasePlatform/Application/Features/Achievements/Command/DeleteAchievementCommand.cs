using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Achievements.Command;

public record DeleteAchievementCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
}

public class DeleteAchievementCommandHandler : IRequestHandler<DeleteAchievementCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteAchievementCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(DeleteAchievementCommand request, CancellationToken cancellationToken)
	{
		var achievement = await _unitOfWork.Repository<Achievement>().GetByIdAsync(request.Id);

		if (achievement == null)
		{
			return Result<bool>.Failure("Không tìm thấy Achievement.");
		}

		await _unitOfWork.Repository<Achievement>().DeleteAsync(achievement);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success(true);
		}
		else
		{
			return Result<bool>.Failure("Không thể xóa Achievement.");
		}
	}
}

