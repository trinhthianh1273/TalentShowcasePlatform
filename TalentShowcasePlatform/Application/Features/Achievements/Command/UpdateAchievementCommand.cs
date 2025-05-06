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

public record UpdateAchievementCommand : IRequest<Result<bool>>
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime? DateAchieved { get; set; }
}

public class UpdateAchievementCommandHandler : IRequestHandler<UpdateAchievementCommand, Result<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateAchievementCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<bool>> Handle(UpdateAchievementCommand request, CancellationToken cancellationToken)
	{
		var achievement = await _unitOfWork.Repository<Achievement>().GetByIdAsync(request.Id);

		if (achievement == null)
		{
			return Result<bool>.Failure("Không tìm thấy Achievement.");
		}

		achievement.UserId = request.UserId;
		achievement.Title = request.Title;
		achievement.Description = request.Description;
		achievement.DateAchieved = request.DateAchieved;

		await _unitOfWork.Repository<Achievement>().UpdateAsync(achievement);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<bool>.Success("Cập nhật thành công");
		}
		else
		{
			return Result<bool>.Failure("Không thể cập nhật thông tin Achievement.");
		}
	}
}

