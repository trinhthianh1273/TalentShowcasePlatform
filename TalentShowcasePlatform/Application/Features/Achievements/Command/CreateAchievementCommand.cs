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

public record CreateAchievementCommand : IRequest<Result<Guid>>
{
	public Guid UserId { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime? DateAchieved { get; set; }
}

public class CreateAchievementCommandHandler : IRequestHandler<CreateAchievementCommand, Result<Guid>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateAchievementCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
	{
		var achievement = new Achievement
		{
			Id = Guid.NewGuid(),
			UserId = request.UserId,
			Title = request.Title,
			Description = request.Description,
			DateAchieved = request.DateAchieved,
			CreatedAt = DateTime.UtcNow
		};

		await _unitOfWork.Repository<Achievement>().AddAsync(achievement);
		var saveResult = await _unitOfWork.Save(cancellationToken);

		if (saveResult > 0)
		{
			return Result<Guid>.Success(achievement.Id);
		}
		else
		{
			return Result<Guid>.Failure("Không thể lưu thông tin Achievement.");
		}
	}
}